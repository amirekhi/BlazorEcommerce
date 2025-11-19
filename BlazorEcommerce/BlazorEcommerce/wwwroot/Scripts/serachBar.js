window.searchInterop = {
    registerOutsideClick: function (dotNetObj, wrapperId) {
        document.addEventListener("click", function (e) {
            const wrapper = document.getElementById(wrapperId);
            if (!wrapper) return;

            if (!wrapper.contains(e.target)) {
                dotNetObj.invokeMethodAsync("CloseSearchFromJs");
            }
        });
    },

    focusElement: function (element) {
        if (element) element.focus();
    },

    registerShortcut: function (dotNetObj) {
        // optional: for keyboard shortcut like Ctrl+K
        document.addEventListener("keydown", function (e) {
            if ((e.ctrlKey || e.metaKey) && e.key.toLowerCase() === "k") {
                e.preventDefault();
                dotNetObj.invokeMethodAsync("ToggleSearchFromJs");
            }
        });
    }
};
