using ET.Extensions;
using ET.Services;
using Hangfire;
using Libs;
using Libs.CacheService;
using Libs.Entity;
using Libs.Repositories;
using Libs.Service;
using Libs.ThanhToan;
using Libs.ThanhToan.Abstractions;
using Libs.ThanhToan.Options;
using Libs.ThanhToan.Providers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.AI;
using Microsoft.IdentityModel.Tokens;
using sendMail.Service;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    //options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

}, ServiceLifetime.Transient);
builder.Services.AddStackExchangeRedisCache(options => {
    options.Configuration = builder.Configuration.GetConnectionString("RedisCacheDoAn");
});
// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        opt.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWTKey:ValidAudience"],
        ValidIssuer = builder.Configuration["JWTKey:ValidIssuer"],
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTKey:Secret"]))
    };
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            context.Token = context.Request.Cookies["jwt"];
            return Task.CompletedTask;
        }
    };
}).AddGoogle(options =>
{
    options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
    options.SignInScheme = IdentityConstants.ExternalScheme;
});

builder.Services.AddControllersWithViews();
//them thanh toan
// options
builder.Services.Configure<MoMoTuyChon>(builder.Configuration.GetSection("MoMo"));
builder.Services.Configure<PayPalTuyChon>(builder.Configuration.GetSection("PayPal"));

// gateways + service
builder.Services.AddScoped<CongMoMo>();
builder.Services.AddScoped<ICongThanhToanFactory, CongThanhToanFactory>();
builder.Services.AddScoped<IThanhToanService, ThanhToanService>();
builder.Services.AddScoped<IDonHangRepository, DonHangRepository>();
builder.Services.AddScoped<IGiaoDichThanhToanRepository, GiaoDichThanhToanRepository>();
builder.Services.AddScoped<ITinhNangMoKhoaRepository, TinhNangMoKhoaRepository>();
builder.Services.AddScoped<ITinhNangService, TinhNangService>();

builder.Services.AddScoped<IDoanhThuRepository, DoanhThuRepository>();
builder.Services.AddScoped<DoanhThuService>();

builder.Services.AddHttpClient();


//
builder.Services.AddTransient<ChuDeService>();
builder.Services.AddTransient<LoaiBangLaiService>();
builder.Services.AddTransient<MoPhongService>();
builder.Services.AddTransient<BaiThiService>();
builder.Services.AddTransient<AdminService>();
builder.Services.AddTransient<CauHoiService>();
builder.Services.AddTransient<SaHinhService>();
builder.Services.AddTransient<BaiThiCache>();
builder.Services.AddTransient<ChuDeCache>();
builder.Services.AddTransient<LoaiBangLaiCache>();
builder.Services.AddTransient<CauHoiCache>();
builder.Services.AddTransient<MoPhongcache>();
builder.Services.AddTransient<SaHinhCache>();
builder.Services.AddTransient<IChuDeRepository, ChuDeRepository>();
builder.Services.AddTransient<ILoaiBangLaiRepository, LoaiBangLaiRepository>();
builder.Services.AddTransient<IBaiThiRepository, BaiThiRepository>();
builder.Services.AddTransient<IGmailSender, GmailSender>();
builder.Services.AddTransient<ILichSuThiRepository, LichSuThiRepository>();
builder.Services.AddTransient<LichSuThiService>();
builder.Services.AddTransient<ChatBoxService>();
builder.Services.AddChatClient(new OpenAI.Chat.ChatClient("gpt-4o-mini", builder.Configuration["OpenAIOptions:APIKey"]).AsIChatClient());
builder.Services.AddHangfire(configuration => configuration
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection"),
        new Hangfire.SqlServer.SqlServerStorageOptions
        {
            SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
            QueuePollInterval = TimeSpan.Zero,
            UseRecommendedIsolationLevel = true,
            UsePageLocksOnDequeue = true,
            DisableGlobalLocks = true
        }));
builder.Services.AddHangfireServer();

//

//
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
    options.AddPolicy("UserPolicy", policy => policy.RequireRole("User"));
});
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();   // <--- BẮT BUỘC PHẢI CÓ
}

app.ApplyMigrations();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.UseHangfireDashboard();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
