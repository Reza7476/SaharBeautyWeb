// ======================= ثبت Service Worker =======================
if ('serviceWorker' in navigator) {
    navigator.serviceWorker.register('/firebase-messaging-sw.js');
}

// ======================= Import Firebase Modules =======================
import { initializeApp } from "https://www.gstatic.com/firebasejs/12.6.0/firebase-app.js";
import { getMessaging, getToken, onMessage } from "https://www.gstatic.com/firebasejs/12.6.0/firebase-messaging.js";
import { getAnalytics } from "https://www.gstatic.com/firebasejs/12.6.0/firebase-analytics.js";

// ======================= تنظیمات Firebase =======================
const firebaseConfig = {
    apiKey: "AIzaSyBmhAlCoJe51viT7yNsNGGjQLW9AsNhBLs",
    authDomain: "beautysalon-f3295.firebaseapp.com",
    projectId: "beautysalon-f3295",
    storageBucket: "beautysalon-f3295.firebasestorage.app",
    messagingSenderId: "1079138885668",
    appId: "1:1079138885668:web:ce3e73d6ef67a7c12bb8b1",
    measurementId: "G-H82MBPEPGF"
};

const app = initializeApp(firebaseConfig);
const analytics = getAnalytics(app);
const messaging = getMessaging(app);


// ======================= Toast زیبا =======================
function showToast(title, message, duration = 5000) {
    const toast = document.createElement('div');
    toast.className = 'custom-toast';
    toast.innerHTML = `
        <div class="toast-header">${title}</div>
        <div class="toast-body">${message}</div>
    `;
    document.body.appendChild(toast);

    // انیمیشن ورود
    setTimeout(() => {
        toast.classList.add('show');
    }, 100);

    // حذف با انیمیشن خروج
    setTimeout(() => {
        toast.classList.remove('show');
        setTimeout(() => toast.remove(), 300);
    }, duration);
}

// ======================= استایل =======================
const style = document.createElement('style');
style.innerHTML = `
    .custom-toast {
        position: fixed;
        bottom: 20px;
        right: 20px;
        min-width: 220px;
        max-width: 300px;
        background-color: rgba(50, 50, 50, 0.9);
        color: #fff;
        padding: 10px 16px;
        border-radius: 12px;
        box-shadow: 0 2px 12px rgba(0,0,0,0.15);
        font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        font-size: 14px;
        opacity: 0;
        transform: translateY(20px);
        transition: all 0.3s ease;
        z-index: 9999;
    }

    .custom-toast.show {
        opacity: 1;
        transform: translateY(0);
    }

    .custom-toast .toast-header {
        font-weight: 600;
        margin-bottom: 4px;
    }

    .custom-toast .toast-body {
        line-height: 1.3;
    }
`;
document.head.appendChild(style);
// ======================= درخواست دسترسی Notification =======================
Notification.requestPermission().then(permission => {
    if (permission === "granted") {
        getToken(messaging,
            {
                vapidKey: "BB2Z4l2PtWkXd_wUKNOFUUKbkNttqnHbGvaDqImMUDrKC817NC6eozLedEYd6nd2r-LKrHTVdXRnRgBjUnEmkLo"
            }).then(token => {
                const antiForgeryToken = $('input[name="__RequestVerificationToken"]').val();
                $.ajax({
                    url: '/UserPanels/Index?handler=SendFireBaseToken',
                    type: 'POST',
                    data: {
                        __RequestVerificationToken: antiForgeryToken,
                        token: token
                    }
                });
            });
    }
});

// ======================= دریافت پیام در Foreground =======================
onMessage(messaging, (payload) => {
    const notificationTitle = payload.notification?.title || 'Notification';
    const type = payload.data.type;
    const receiver = payload.data.receiver;

    const notificationOptions = {
        body: payload.notification?.body || '',
        icon: 'https://upload.wikimedia.org/wikipedia/commons/7/73/Flat_tick_icon.svg',
        data: {
           // url: getUrl(type, receiver)
        }
    };

    if (Notification.permission === "granted") {
        const notification = new Notification(notificationTitle, notificationOptions);
        showToast(payload.notification.title, payload.notification.body);
    }
});

