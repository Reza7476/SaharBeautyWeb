

// Simple Horizontal Scroll for Gallery & Services (Mouse Wheel)
const horizontalScroll = (selector) => {
    const container = document.querySelector(selector);
    if (container) {
        container.addEventListener('wheel', (e) => {
            e.preventDefault();
            container.scrollLeft += e.deltaY;
        });
    }
}
horizontalScroll('.services-wrapper');
horizontalScroll('.gallery-slider');
horizontalScroll('.testimonial-slider');


const mobileButton = document.getElementById('mobile-menu-button');
const mobileMenu = document.getElementById('mobile-menu');

mobileButton.addEventListener('click', () => {
    mobileMenu.classList.toggle('show');
});