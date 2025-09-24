window.analytics = {
    collectVisitorMetadata: function () {
        // Generate or fetch sessionId
        let sessionId = sessionStorage.getItem('session-id');
        if (!sessionId) {
            sessionId = crypto.randomUUID();
            sessionStorage.setItem('session-id', sessionId);
        }

        return {
            sessionId: sessionId,
            deviceType: getDeviceType(),
            pageVisited: window.location.pathname
        };
    }
};

function getDeviceType() {
    const ua = navigator.userAgent;
    if (/Mobi|Android/i.test(ua)) {
        return 'Mobile';
    } else if (/Tablet|iPad/i.test(ua)) {
        return 'Tablet';
    } else {
        return 'Desktop';
    }
}
