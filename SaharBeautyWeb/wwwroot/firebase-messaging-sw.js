// firebase-messaging-sw.js
importScripts('https://www.gstatic.com/firebasejs/9.23.0/firebase-app-compat.js');
importScripts('https://www.gstatic.com/firebasejs/9.23.0/firebase-messaging-compat.js');

// Initialize Firebase
firebase.initializeApp({
    apiKey: "AIzaSyBmhAlCoJe51viT7yNsNGGjQLW9AsNhBLs",
    authDomain: "beautysalon-f3295.firebaseapp.com",
    projectId: "beautysalon-f3295",
    storageBucket: "beautysalon-f3295.firebasestorage.app",
    messagingSenderId: "1079138885668",
    appId: "1:1079138885668:web:ce3e73d6ef67a7c12bb8b1",
    measurementId: "G-H82MBPEPGF"
});

// Retrieve an instance of Firebase Messaging
const messaging = firebase.messaging();

messaging.onBackgroundMessage(function (payload) {
    const notificationTitle = payload.notification.title;
    const type = payload.data.type; // نوع نوتیف
    const receiver = payload.data.receiver; // admin یا user

    const notificationOptions = {
        body: payload.notification.body,
        icon: 'https://upload.wikimedia.org/wikipedia/commons/7/73/Flat_tick_icon.svg',
        data: {
            url: getUrl(type, receiver) //getNotificationUrl(type, receiver)
        }
    };
    self.registration.showNotification(notificationTitle, notificationOptions);
});

self.addEventListener('notificationclick', function (event) {
    event.notification.close(); // حذف نوتیف بعد از کلیک

    // باز کردن مسیر مشخص
    event.waitUntil(
        clients.matchAll({ type: 'window', includeUncontrolled: true }).then(windowClients => {
            // اگر تب با مسیر وجود داشت، روی آن فعال می‌کنیم
            for (let client of windowClients) {
                if (client.url.includes(event.notification.data.url) && 'focus' in client) {
                    return client.focus();
                }
            }
            // در غیر این صورت تب جدید باز می‌کنیم
            if (clients.openWindow) {
                return clients.openWindow(event.notification.data.url);
            }
        })
    );
});


function getUrl(type, receiver) {

    let baseUrl;

    if (receiver === "Admin") {
        baseUrl = '/UserPanels/Admin';

        switch (type) {
            case "NewAppointment":
                return `${baseUrl}/Appointments/PendingAppointments`;
        }

        return baseUrl;
    }

    if (receiver === "Client") {
        baseUrl = '/UserPanels/Client';

        switch (type) {
            case "NewMessage":
                return `${baseUrl}/Messages`;
        }

        return baseUrl;
    }

    return '/';
}