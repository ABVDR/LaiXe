// Hàm lấy cookie
function getCookie(name) {
    const value = `; ${document.cookie}`;
    const parts = value.split(`; ${name}=`);
    if (parts.length === 2) return parts.pop().split(';').shift();
}

// Hàm hiển thị thông báo lỗi
function showError(elementId, message) {
    const errorElement = document.getElementById(elementId);
    errorElement.textContent = message;
    errorElement.style.display = 'block';
}

// Đăng ký
async function register(event) {
    event.preventDefault();

    const username = document.getElementById('Username').value.trim();
    const email = document.getElementById('Email').value.trim();
    const password = document.getElementById('Password').value;
    const confirmPassword = document.getElementById('ConfirmPassword').value;

    // Kiểm tra mật khẩu có tối thiểu 6 ký tự và có số
    const passwordValid = password.length >= 6 && /\d/.test(password);
    if (!passwordValid) {
        showError('registerError', 'Mật khẩu phải có ít nhất 6 ký tự và chứa ít nhất một chữ số.');
        return;
    }

    // Kiểm tra xác nhận mật khẩu
    if (password !== confirmPassword) {
        showError('registerError', 'Xác nhận mật khẩu không khớp.');
        return;
    }

    try {
        const response = await fetch('/api/authenticate/register', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            credentials: 'include',
            body: JSON.stringify({ username, email, password })
        });

        const result = await response.json();
        if (result.status) {
            window.alert('Đăng ký thành công! Vui lòng đăng nhập lại.');
            window.location.href = '/';
        } else {
            showError('registerError', result.message || 'Đăng ký thất bại');
        }
    } catch (error) {
        showError('registerError', 'Lỗi kết nối server');
    }
}


// Đăng nhập
async function login(event) {
    event.preventDefault();
    const username = document.getElementById('Username').value;
    const password = document.getElementById('Password').value;

    try {
        const response = await fetch('/api/authenticate/login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            credentials: 'include',
            body: JSON.stringify({ username, password })
        });

        const result = await response.json();
        if (result.status) {
            if (result.roles.includes('Admin')) {
                window.location.href = '/Admin/Index';
            } else {
                window.location.href = '/Home/Index';
            }
        } else {
            showError('loginError', result.message || 'Đăng nhập thất bại');
        }
    } catch (error) {
        showError('loginError', 'Lỗi kết nối server');
    }
}

// Đăng xuất
async function logout(event) {
    event.preventDefault();
    try {
        await fetch('/api/authenticate/logout', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            credentials: 'include'
        });
        window.location.href = '/';
    } catch (error) {
        console.error('Lỗi đăng xuất:', error);
    }
}

// Gọi API bảo vệ
async function callProtectedApi() {
    const token = getCookie('jwt');
    try {
        const response = await fetch('/api/values', {
            headers: {
                'Authorization': `Bearer ${token}`
            },
            credentials: 'include'
        });
        const data = await response.json();
        console.log(data);
    } catch (error) {
        console.error('Lỗi gọi API:', error);
    }
}
async function googleLogin() {
    try {
        // Navigate to the google-login endpoint to start the OAuth flow
        window.location.href = '/api/authenticate/google-login';
    } catch (error) {
        showError('loginError', 'Lỗi khi bắt đầu đăng nhập Google');
    }
}