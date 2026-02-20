const API_URL = 'https://api-josuealvarez20264345.onrender.com/api';

// Import cart functions
import { updateCartCount } from './cart.js';

// Utility to fetch with Auth
async function fetchWithAuth(url, options = {}) {
    const token = localStorage.getItem('token');
    const headers = {
        'Content-Type': 'application/json',
        ...options.headers
    };

    if (token) {
        headers['Authorization'] = `Bearer ${token}`;
    }

    const response = await fetch(`${API_URL}${url}`, {
        ...options,
        headers
    });

    if (response.status === 401) {
        logout();
        return null;
    }

    return response;
}

function logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    window.location.href = 'login.html';
}
window.logout = logout;

function checkAuth() {
    return !!localStorage.getItem('token');
}

function getUser() {
    const user = localStorage.getItem('user');
    return user ? JSON.parse(user) : null;
}

// UI Rendering
function renderHeader() {
    const nav = document.getElementById('main-nav');
    const isLoggedIn = checkAuth();
    const user = getUser();
    const isAdmin = user && user.role === 'Admin';

    let links = `
        <li><a href="index.html">Tienda</a></li>
    `;

    if (isLoggedIn) {
        if (isAdmin) {
            links += `<li><a href="admin.html">Admin</a></li>`;
        }
        links += `<li><a href="#" onclick="logout()">Cerrar Sesión</a></li>`;
    } else {
        links += `<li><a href="login.html">Login</a></li>`;
    }

    // Add Cart link
    links += `<li><a href="cart.html">🛒 <span id="cart-count">0</span></a></li>`;

    nav.innerHTML = `
        <div class="logo">SexShop Deluxe</div>
        <ul class="nav-links">${links}</ul>
    `;

    // Update cart count after rendering
    updateCartCount();
}

document.addEventListener('DOMContentLoaded', renderHeader);

