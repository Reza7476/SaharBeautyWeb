// ------------------------------
// Sidebar / Menu Script (Optimized)
// ------------------------------

let sidebarOpen = true;
let openSubmenu = null;

// Elements
const sidebar = document.getElementById("sidebar");
const sidebarOverlay = document.getElementById("sidebar-overlay");
const toggleSidebarBtn = document.getElementById("toggle-sidebar");
const closeSidebarBtn = document.getElementById("close-sidebar");
const mainContent = document.getElementById("main-content");

// ------------------------------
// Initialization
// ------------------------------
window.addEventListener("DOMContentLoaded", () => {
    // Auto-collapse on mobile
    if (window.innerWidth < 768) {
        closeSidebar();
    }

    // Restore last opened page submenu (optional)
    const savedSubmenu = localStorage.getItem("openSubmenu");
    if (savedSubmenu) {
        const submenu = document.getElementById(`submenu-${savedSubmenu}`);
        const icon = document.getElementById(`icon-${savedSubmenu}`);
        if (submenu && icon) {
            submenu.classList.add("open");
            icon.classList.add("rotate-180");
            openSubmenu = savedSubmenu;
        }
    }
});

// ------------------------------
// Sidebar Toggle
// ------------------------------
function toggleSidebar() {
    sidebarOpen ? closeSidebar() : openSidebar();
}

function openSidebar() {
    sidebarOpen = true;
    sidebar.classList.remove("closed");
    sidebarOverlay.classList.remove("hidden");

    if (window.innerWidth >= 768) {
        mainContent.classList.remove("mr-0");
        mainContent.classList.add("mr-80");
    }
}

function closeSidebar() {
    sidebarOpen = false;
    sidebar.classList.add("closed");
    sidebarOverlay.classList.add("hidden");

    if (window.innerWidth >= 768) {
        mainContent.classList.remove("mr-80");
        mainContent.classList.add("mr-0");
    }
}

// ------------------------------
// Submenu Toggle
// ------------------------------
function toggleSubmenu(menuId) {
    const submenu = document.getElementById(`submenu-${menuId}`);
    const icon = document.getElementById(`icon-${menuId}`);
    if (!submenu || !icon) return;

    // Close previous submenu if open
    if (openSubmenu && openSubmenu !== menuId) {
        const prevSubmenu = document.getElementById(`submenu-${openSubmenu}`);
        const prevIcon = document.getElementById(`icon-${openSubmenu}`);
        if (prevSubmenu) prevSubmenu.classList.remove("open");
        if (prevIcon) prevIcon.classList.remove("rotate-180");
    }

    // Toggle current submenu
    const isOpen = submenu.classList.toggle("open");
    icon.classList.toggle("rotate-180");

    openSubmenu = isOpen ? menuId : null;
    localStorage.setItem("openSubmenu", openSubmenu || "");
}

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

    //if (confirm("آیا مطمئن هستید که می‌خواهید خارج شوید؟")) {
    //    localStorage.removeItem("openSubmenu");
    //    window.location.href = "/Account/Logout";
    //}
}

// ------------------------------
// Event Listeners
// ------------------------------
toggleSidebarBtn?.addEventListener("click", toggleSidebar);
closeSidebarBtn?.addEventListener("click", closeSidebar);
sidebarOverlay?.addEventListener("click", closeSidebar);

// ------------------------------
// Responsive behavior
// ------------------------------
window.addEventListener("resize", () => {
    if (window.innerWidth >= 768) openSidebar();
    else closeSidebar();
});

$(document).on("click", "#log-out-btn", function (e) {

   
})