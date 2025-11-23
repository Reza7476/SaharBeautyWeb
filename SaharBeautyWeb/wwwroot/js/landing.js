$(() => {

    // --- اسکرول افقی کارت‌ها با موس (Mouse Wheel) ---
    $(document).on('wheel', '.scroll-container', function (e) {
        e.preventDefault();
        this.scrollLeft += e.originalEvent.deltaY;
    });

    // --- Prev / Next دکمه‌ها ---
    $(document).on('click', '.scroll-btn.prev', function () {
        const container = $(this).closest('.scroll-wrapper').find('.scroll-container')[0];
        if (container) container.scrollBy({ left: -300, behavior: 'smooth' });
    });

    $(document).on('click', '.scroll-btn.next', function () {
        const container = $(this).closest('.scroll-wrapper').find('.scroll-container')[0];
        if (container) container.scrollBy({ left: 300, behavior: 'smooth' });
    });

    // --- موبایل منو ---
    const mobileButton = document.getElementById('mobile-menu-button');
    const mobileMenu = document.getElementById('mobile-menu');
    if (mobileButton && mobileMenu) {
        mobileButton.addEventListener('click', () => {
            mobileMenu.classList.toggle('show');
        });
    }

    // --- AJAX Pagination ---
    $(document).on('click', '.page-btn', function (e) {
        e.preventDefault();

        const container = $(this).closest('.client-comments-component');
        var page = parseInt($(this).data('page'));
        if (isNaN(page) || page < 0) return;

        $.ajax({
            url: '/?handler=ClientComments',
            type: 'GET',
            data: { pageNumber: page },
            beforeSend: function () {
                container.find('.page-btn').prop('disabled', true);
            },
            success: function (html) {
                container.html(html);
            },
            complete: function () {
                container.find('.page-btn').prop('disabled', false);
            }
        });
    });

    $(document).on('click', '.page-btn-gallery', function (e) {
        e.preventDefault();

        const container = $(this).closest('.treatment-gallery-containers');
        var page = parseInt($(this).data('page'));
        if (isNaN(page) || page < 0) return;

        $.ajax({
            url: '/?handler=LandingGallery',
            type: 'GET',
            data: { pageNumber: page },
            beforeSend: function () {
                container.find('.page-btn-gallery').prop('disabled', true);
            },
            success: function (html) {
                container.html(html);
            },
            complete: function () {
                container.find('.page-btn-gallery').prop('disabled', false);
            }
        });
    });



});
