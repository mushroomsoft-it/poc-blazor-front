// Filter Panel - Dropdown positioning helper
window.filterPanelInterop = {
    // Position the dropdown to avoid viewport overflow
    positionDropdown: function (buttonId, dropdownId) {
        const button = document.getElementById(buttonId);
        const dropdown = document.getElementById(dropdownId);

        if (!button || !dropdown) return;

        const buttonRect = button.getBoundingClientRect();
        const dropdownRect = dropdown.getBoundingClientRect();
        const viewportWidth = window.innerWidth;
        const viewportHeight = window.innerHeight;

        // Reset position
        dropdown.style.left = '0';
        dropdown.style.right = 'auto';

        // Check if dropdown goes off the right edge
        if (buttonRect.left + dropdownRect.width > viewportWidth - 20) {
            dropdown.style.left = 'auto';
            dropdown.style.right = '0';
        }

        // Check if dropdown goes off the bottom edge
        if (buttonRect.bottom + dropdownRect.height > viewportHeight - 20) {
            dropdown.style.top = 'auto';
            dropdown.style.bottom = 'calc(100% + 4px)';
        }
    }
};
