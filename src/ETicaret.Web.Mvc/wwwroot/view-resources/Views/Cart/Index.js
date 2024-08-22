document.addEventListener('DOMContentLoaded', function () {
    let cart = JSON.parse(localStorage.getItem('cart')) || [];
    renderCart(cart);

    function renderCart(cart) {
        let cartItems = document.getElementById('cart-items');
        cartItems.innerHTML = '';
        let totalPrice = 0;

        cart.forEach(item => {
            let itemTotalPrice = item.price * item.quantity;
            totalPrice += itemTotalPrice;

            let row = `
                                <tr>
                                    <td>${item.name}<br><img src="${item.image}" alt="${item.name}" width="50"></td>
                                    <td>${item.price.toLocaleString('tr-TR', { style: 'currency', currency: 'TRY' })}</td>
                                    <td>
                                        <button class="btn btn-sm btn-outline-primary decrease-quantity" data-id="${item.id}">-</button>
                                        <span>${item.quantity}</span>
                                        <button class="btn btn-sm btn-outline-primary increase-quantity" data-id="${item.id}">+</button>
                                    </td>
                                    <td>${itemTotalPrice.toLocaleString('tr-TR', { style: 'currency', currency: 'TRY' })}</td>
                                    <td><button class="btn btn-sm btn-danger remove-item" data-id="${item.id}">X</button></td>
                                </tr>
                            `;
            cartItems.insertAdjacentHTML('beforeend', row);
        });

        document.getElementById('cart-total-price').textContent = totalPrice.toLocaleString('tr-TR', {
            style: 'currency',
            currency: 'TRY'
        });

        attachEventListeners();

        updateCartTotal();
    }

    function attachEventListeners() {
        document.querySelectorAll('.remove-item').forEach(button => {
            button.addEventListener('click', function () {
                const productId = this.getAttribute('data-id');
                let cart = JSON.parse(localStorage.getItem('cart')) || [];
                cart = cart.filter(item => item.id !== productId);
                localStorage.setItem('cart', JSON.stringify(cart));
                renderCart(cart);
                toastr.danger('Ürün sepetten silindi!');
            });
        });

        document.querySelectorAll('.increase-quantity').forEach(button => {
            button.addEventListener('click', function () {
                const productId = this.getAttribute('data-id');
                let cart = JSON.parse(localStorage.getItem('cart')) || [];
                let product = cart.find(item => item.id === productId);
                if (product) {
                    product.quantity++;
                    localStorage.setItem('cart', JSON.stringify(cart));
                    renderCart(cart);
                }
            });
        });

        document.querySelectorAll('.decrease-quantity').forEach(button => {
            button.addEventListener('click', function () {
                const productId = this.getAttribute('data-id');
                let cart = JSON.parse(localStorage.getItem('cart')) || [];
                let product = cart.find(item => item.id === productId);
                if (product && product.quantity > 1) {
                    product.quantity--;
                    localStorage.setItem('cart', JSON.stringify(cart));
                    renderCart(cart);
                }
            });
        });
    }

    function updateCartTotal() {
        let cart = JSON.parse(localStorage.getItem('cart')) || [];
        let totalAmount = cart.reduce((total, product) => total + product.price * product.quantity, 0);

        document.getElementById('navbar-cart-total').textContent = totalAmount.toLocaleString('tr-TR', {
            style: 'currency',
            currency: 'TRY'
        });
    }

    updateCartTotal();
});