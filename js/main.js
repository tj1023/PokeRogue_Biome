document.addEventListener('DOMContentLoaded', () => {
    createLocationGrid();

    document.getElementById('searchBtn').addEventListener('click', findPaths);

    document.getElementById('startInput').addEventListener('keypress', function(e) {
        if (e.key === 'Enter') {
            document.getElementById('endInput').focus();
        }
    });

    document.getElementById('endInput').addEventListener('keypress', function(e) {
        if (e.key === 'Enter') {
            findPaths();
        }
    });
});
