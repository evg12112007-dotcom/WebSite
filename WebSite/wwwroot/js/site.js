
document.addEventListener('DOMContentLoaded', function ()
{
    let lastScrollY = window.scrollY;
    const header = document.getElementById('header');

    window.addEventListener('scroll', () => {
        if (window.scrollY > lastScrollY && window.scrollY > 100) {
            // Скролл вниз - скрываем
            header.classList.add('hidden');
        } else {
            // Скролл вверх - показываем
            header.classList.remove('hidden');
        }
        lastScrollY = window.scrollY;
    });
});
