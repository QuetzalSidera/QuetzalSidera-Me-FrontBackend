export function waitForTransitionEnd(element) {
    return new Promise((resolve) => {
        const onTransitionEnd = () => {
            element.removeEventListener('transitionend', onTransitionEnd);
            resolve();
        };
        element.addEventListener('transitionend', onTransitionEnd);

        // 备用超时（防止transitionend事件不触发）
        setTimeout(resolve, 600);
    });
}