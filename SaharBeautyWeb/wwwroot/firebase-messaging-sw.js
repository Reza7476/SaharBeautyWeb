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
    const notificationOptions = {
        body: payload.notification.body,
        icon: 'https://upload.wikimedia.org/wikipedia/commons/7/73/Flat_tick_icon.svg'
    };
    self.registration.showNotification(notificationTitle, notificationOptions);
});


