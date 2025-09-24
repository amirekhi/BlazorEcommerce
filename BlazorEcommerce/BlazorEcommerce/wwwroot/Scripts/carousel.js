window.carouselScroll = (carouselElement, direction) => {
    console.log("carouselScroll called", carouselElement, direction);

    if (!carouselElement) {
        console.error("carouselElement is null or undefined");
        return;
    }

    const scrollAmount = 300 * direction;
    carouselElement.scrollBy({
        left: scrollAmount,
        behavior: 'smooth'
    });
};



    window.searchInterop = {
        registerShortcut: function (dotNetRef) {
            document.addEventListener('keydown', function (e) {
                if ((e.ctrlKey || e.metaKey) && e.key.toLowerCase() === 'k') {
                    e.preventDefault();
                    dotNetRef.invokeMethodAsync('ToggleSearchFromJs');
                }
            });
        },
        focusElement: function (element) {
            if (element) {
                element.focus();
            }
        }
    };

