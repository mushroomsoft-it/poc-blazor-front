window.closeDropdown = function (dropdownId) {
    const dropdownElement = document.getElementById(dropdownId);
    if (dropdownElement) {
        const dropdown = bootstrap.Dropdown.getInstance(dropdownElement);
        if (dropdown) {
            dropdown.hide();
        }
    }
};
