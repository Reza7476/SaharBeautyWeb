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



// ======================= Toast داخلی =======================
function showToast(title, message) {
    const toast = document.createElement('div');
    toast.className = 'custom-toast';
    toast.innerHTML = `<strong>${title}</strong><p>${message}</p>`;
    document.body.appendChild(toast);
    setTimeout(() => toast.remove(), 10000);
}

// استایل ساده برای Toast
const style = document.createElement('style');
style.innerHTML = `
                .custom-toast {
                    position: fixed;
                    bottom: 20px;
                    right: 20px;
                    background-color: #333;
                    color: #fff;
                    padding: 12px 18px;
                    border-radius: 6px;
                    box-shadow: 0 4px 8px rgba(0,0,0,0.2);
                    z-index: 9999;
                    font-family: sans-serif;
                    opacity: 0.95;
                }
            `;
document.head.appendChild(style);

// ======================= درخواست دسترسی Notification =======================
Notification.requestPermission().then(permission => {
    if (permission === "granted") {
        // گرفتن FCM Token
        getToken(messaging, {
            vapidKey: "BB2Z4l2PtWkXd_wUKNOFUUKbkNttqnHbGvaDqImMUDrKC817NC6eozLedEYd6nd2r-LKrHTVdXRnRgBjUnEmkLo"
        }).then(token => {

            // ارسال Token به سرور
            const antiForgeryToken = $('input[name="__RequestVerificationToken"]').val();
            $.ajax({
                url: '/UserPanels/Index?handler=SendFireBaseToken',
                type: 'POST',
                data: {
                    __RequestVerificationToken: antiForgeryToken,
                    token: token
                },
                success: function (res) {
                },
                error: function (err) {
                }
            });

        }).catch(err => {
        });
    } else {
    }
});

// ======================= دریافت پیام در Foreground =======================
onMessage(messaging, (payload) => {

    if (Notification.permission === "granted" && payload.notification) {
        new Notification(payload.notification.title, {
            body: payload.notification.body,
            icon: 'https://upload.wikimedia.org/wikipedia/commons/7/73/Flat_tick_icon.svg'
        });
    }

    // نمایش Toast داخلی در UI
    if (payload.notification) {
        showToast(payload.notification.title, payload.notification.body);
    }
});