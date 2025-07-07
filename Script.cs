/* ---------- DROP PASSWORD ---------- */
const PASSWORD = "drop2025";
const gate      = document.getElementById("passwordGate");
const gateBtn   = document.getElementById("gateBtn");
const gateInput = document.getElementById("gateInput");
const gateError = document.getElementById("gateError");

if (!sessionStorage.getItem("passedGate")) gate.classList.remove("hidden");
gateBtn.addEventListener("click", () => {
  if (gateInput.value === PASSWORD) {
    sessionStorage.setItem("passedGate", "true");
    gate.classList.add("hidden");
  } else {
    gateError.textContent = "Wrong Password";
  }
});

/* ---------- COUNTDOWN ---------- */
const dropDate    = new Date("2025-07-31T18:00:00+05:30").getTime();
const countdownEl = document.getElementById("countdown");
setInterval(() => {
  const now  = Date.now();
  let diff   = dropDate - now;  if (diff < 0) diff = 0;
  const d = Math.floor(diff / 86e6),
        h = Math.floor(diff % 86e6 / 36e5),
        m = Math.floor(diff % 36e5 / 6e4),
        s = Math.floor(diff % 6e4 / 1000);
  countdownEl.textContent = `${d}d ${h}h ${m}m ${s}s`;
}, 1000);

/* ---------- CART ---------- */
let cart = JSON.parse(localStorage.getItem("cart") || "[]");
const cartBtn   = document.getElementById("cartBtn");
const cartModal = document.getElementById("cartModal");
const closeCart = document.getElementById("closeCart");
const cartItems = document.getElementById("cartItems");
const cartCount = document.getElementById("cartCount");
const cartTotal = document.getElementById("cartTotal");
const addBtns   = document.querySelectorAll(".addBtn");

function renderCart() {
  cartItems.innerHTML = "";
  let total = 0;
  cart.forEach((item, i) => {
    total += item.price;
    const p = document.createElement("p");
    p.innerHTML = `${item.name} – ₹${item.price} <span data-i="${i}" class="remove" style="cursor:pointer">✕</span>`;
    cartItems.appendChild(p);
  });
  cartTotal.textContent = total;
  cartCount.textContent = cart.length;
  localStorage.setItem("cart", JSON.stringify(cart));
}
renderCart();

addBtns.forEach(btn => btn.addEventListener("click", () => {
  const prod = btn.parentElement;
  cart.push({ id: prod.dataset.id, name: prod.dataset.name, price: +prod.dataset.price });
  renderCart();
  cartModal.classList.remove("hidden");
}));

cartItems.addEventListener("click", e => {
  if (e.target.classList.contains("remove")) {
    cart.splice(e.target.dataset.i, 1);
    renderCart();
  }
});

cartBtn  .addEventListener("click", () => cartModal.classList.remove("hidden"));
closeCart.addEventListener("click", () => cartModal.classList.add("hidden"));

/* ---------- CHECKOUT ---------- */
const checkoutBtn  = document.getElementById("checkoutBtn");
const orderModal   = document.getElementById("orderModal");
const closeOrder   = document.getElementById("closeOrder");
const orderForm    = document.getElementById("orderForm");
const upiSection   = document.getElementById("upiSection");
const orderSuccess = document.getElementById("orderSuccess");

checkoutBtn.addEventListener("click", () => {
  if (!cart.length) return alert("Cart is empty!");
  cartModal.classList.add("hidden");
  orderModal.classList.remove("hidden");
});
closeOrder.addEventListener("click", () => orderModal.classList.add("hidden"));

orderForm.addEventListener("submit", e => {
  e.preventDefault();
  upiSection .classList.remove("hidden");
  orderSuccess.textContent = "Order placed! Pay above & email screenshot.";
  cart = [];
  renderCart();
});