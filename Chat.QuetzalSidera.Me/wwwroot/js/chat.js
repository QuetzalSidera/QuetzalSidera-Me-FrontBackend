// ====================== 自动调整输入框高度 ======================
document.addEventListener("input", function (e) {
    const input = e.target;
    if (input.classList.contains("message-input")) {
        adjustInputHeight(input);
    }
});

document.addEventListener("keydown", function (e) {
    const input = e.target;
    if (input.classList.contains("message-input") && e.key === "Enter") {
        // 回车瞬间先调整一次高度（去除末尾换行）
        adjustInputHeight(input, true);
    }
});

// 统一的调整逻辑
function adjustInputHeight(input, isEnter = false) {
    // 暂存真实内容
    let realValue = input.value;

    // 如果是回车触发，就去掉末尾换行以免多一行
    if (isEnter) realValue = realValue.replace(/\n+$/, "");

    // 计算高度
    const tempValue = realValue || " ";
    input.value = tempValue;
    input.style.height = "auto";
    input.style.height = input.scrollHeight + "px";
    input.value = input.value === tempValue ? realValue : input.value;
}

function scrollChatToBottom() {
    const chatContainer = document.querySelector(".chat-messages");
    if (chatContainer) {
        chatContainer.scrollTo({
            top: chatContainer.scrollHeight,
            behavior: "smooth"
        });
    }
}

window.preventEnterDefault = function (event) {
    if (event.key === "Enter" && !event.shiftKey) {
        event.preventDefault();
    }
}

// 供 Blazor 调用的函数
window.chatHelper = {
    scrollToBottom: scrollChatToBottom
};
