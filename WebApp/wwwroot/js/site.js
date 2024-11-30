window.checkTextTruncation = (element) => {
    if (!element) return { scrollWidth: 0, clientWidth: 0 };
    return { scrollWidth: element.scrollWidth, clientWidth: element.clientWidth };
};