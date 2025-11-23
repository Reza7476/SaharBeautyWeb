$(() => {

    // --- اسکرول افقی کارت‌ها با موس (Mouse Wheel) ---
    $(document).on('wheel', '#scrollBox', function (e) {
        e.preventDefault();
        this.scrollLeft += e.originalEvent.deltaY;
    });

    // --- Prev / Next دکمه‌ها ---
    $(document).on('click', '#prevBtn', function () {
        const scrollBox = document.getElementById('scrollBox');
        if (scrollBox) scrollBox.scrollBy({ left: 300, behavior: 'smooth' });
    });

    $(document).on('click', '#nextBtn', function () {
        const scrollBox = document.getElementById('scrollBox');
        if (scrollBox) scrollBox.scrollBy({ left: -300, behavior: 'smooth' });
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

        var page = parseInt($(this).data('page'));
        if (isNaN(page) || page < 0) return;

        $.ajax({
            url: '/?handler=ClientComments', // PageHandler برای ViewComponent
            type: 'GET',
            data: { pageNumber: page },
            beforeSend: function () {
                $('.page-btn').prop('disabled', true); // جلوگیری از کلیک چندباره
            },
            success: function (html) {
                $('#client-comments-container').html(html);
            },
            complete: function () {
                $('.page-btn').prop('disabled', false);
            }
        });
    });

});


