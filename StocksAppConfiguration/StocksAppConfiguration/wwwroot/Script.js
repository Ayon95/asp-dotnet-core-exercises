const stockPriceElement = document.getElementById("stock-price");
const stockSymbolElement = document.getElementById("stock-symbol");
const tokenElement = document.getElementById("token");

const socket = new WebSocket(`wss://ws.finnhub.io?token=${tokenElement.textContent}`);

// Connection opened - subscribe to a stock symbol
socket.addEventListener("open", function () {
    const socketData = { type: "subscribe", symbol: stockSymbolElement.textContent };
    socket.send(JSON.stringify(socketData));
});
// Listen for messages
socket.addEventListener("message", function (e) {
    const stockPriceData = JSON.parse(e.data).data;
    const latestPrice = stockPriceData.p;
    stockPriceElement.textContent = latestPrice;
});

function unsubscribe() {
    const socketData = { type: "unsubscribe", symbol: stockSymbolElement.textContent };
    socket.send(JSON.stringify(socketData));
}

window.addEventListener("beforeunload", unsubscribe);

