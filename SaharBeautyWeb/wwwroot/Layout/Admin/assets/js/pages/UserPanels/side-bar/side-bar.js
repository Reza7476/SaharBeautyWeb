// ------------------------------
// Logout
// ------------------------------
function logout() {

    $.ajax({
        url: '/Auth/Login?handler=RemoveToken',
        type: 'get',
        success: function (res) {
            if (res.sussess || res.statusCode === 200) {
                window.location.href = '/';
            } else {
                handleApiError(res.error);
            }
        }, error: function (err) {
            handleApiError(err);
        }
    });
}

document.addEventListener("DOMContentLoaded", function () {
    const sidebar = document.getElementById("sidebar");
    const overlay = document.getElementById("sidebar-overlay");
    const toggleBtn = document.getElementById("toggle-sidebar");
    const toggleIcon = toggleBtn.querySelector("i");
    const closeBtn = document.getElementById("close-sidebar"); // <-- این خط
    let sidebarOpen = false; // وضعیت منو در موبایل

    function openSidebar() {
        sidebar.classList.remove("translate-x-full");
        overlay.classList.remove("hidden");
        toggleIcon.classList.remove("fa-bars");
        toggleIcon.classList.add("fa-times");
        sidebarOpen = true;
    }

    function closeSidebar() {
        sidebar.classList.add("translate-x-full");
        overlay.classList.add("hidden");
        toggleIcon.classList.remove("fa-times");
        toggleIcon.classList.add("fa-bars");
        sidebarOpen = false;
    }

    toggleBtn.addEventListener("click", function () {
        if (sidebarOpen) {
            closeSidebar();
        } else {
            openSidebar();
        }
    });

    overlay.addEventListener("click", closeSidebar);
    closeBtn?.addEventListener("click", closeSidebar);
    // حالت اولیه بر اساس رزولوشن
    function handleResponsive() {
        if (window.innerWidth >= 768) {
            sidebar.classList.remove("translate-x-full");
            overlay.classList.add("hidden");
            toggleIcon.classList.remove("fa-times");
            toggleIcon.classList.add("fa-bars");
            sidebarOpen = false;
        } else {
            sidebar.classList.add("translate-x-full");
            sidebarOpen = false;
        }
    }

    handleResponsive();
    window.addEventListener("resize", handleResponsive);



});
function toggleSubmenu(id) {
    const submenu = document.getElementById(`submenu-${id}`);
    const icon = document.getElementById(`icon-${id}`);
    if (submenu.classList.contains("open")) {
        submenu.classList.remove("open");
        icon.classList.remove("rotate-180"); // برای چرخش فلش
    } else {
        submenu.classList.add("open");
        icon.classList.add("rotate-180");
    }
}

