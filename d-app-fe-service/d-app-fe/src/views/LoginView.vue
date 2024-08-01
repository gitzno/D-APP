<script setup>
import axios from "axios";
import router from "@/router";
</script>
<template lang="">
    <div class="bg-card">
        <div class="card">
            <div class="login">
                <h2 class="login-header">Đăng Nhập Ngay</h2>
                <p style="color: red; text-align: center">{{ message }}</p>
                <form @submit="handleSubmit" class="login-container">
                    <p>
                        <input
                            v-model="username"
                            type="text"
                            placeholder="Tài Khoản"
                        />
                    </p>
                    <p>
                        <input
                            v-model="password"
                            type="password"
                            placeholder="Mật Khẩu"
                        />
                    </p>
                    <p><input type="submit" value="Đăng Nhập" /></p>
                </form>
            </div>
        </div>
    </div>
</template>
<style>
.login {
    width: 400px;
    margin: 16px auto;
    font-size: 16px;
}

/* Reset top and bottom margins from certain elements */
.login-header,
.login p {
    margin-top: 0;
    margin-bottom: 0;
}

.login-header {
    background: rgb(0, 0, 0);
    padding: 20px;
    font-size: 1.4em;
    font-weight: normal;
    text-align: center;
    text-transform: uppercase;
    color: #fff;
}

.login-container {
    background: #ebebeb;
    padding: 12px;
}

/* Every row inside .login-container is defined with p tags */
.login p {
    padding: 12px;
}

.login input {
    box-sizing: border-box;
    display: block;
    width: 100%;
    border-width: 1px;
    border-style: solid;
    padding: 16px;
    outline: 0;
    font-family: inherit;
    font-size: 0.95em;
}

.login input[type="email"],
.login input[type="password"] {
    background: #fff;
    border-color: #bbb;
    color: #555;
}

/* Text fields' focus effect */
.login input[type="email"]:focus,
.login input[type="password"]:focus {
    border-color: #888;
}

.login input[type="submit"] {
    background: rgb(0, 0, 0);
    border-color: transparent;
    color: #fff;
    cursor: pointer;
}

.login input[type="submit"]:hover {
    background: rgb(85, 85, 85);
}

/* Buttons' focus effect */
.login input[type="submit"]:focus {
    border-color: rgb(0, 0, 0);
}
.bg-card {
    min-height: 50vh;
}
</style>

<script>
const link = "http://localhost:5217/api/Users/Login";
export default {
    name: "login",
    data() {
        return {
            message: "",
            username: "",
            password: "",
        };
    },

    props: ["token"],

    async beforeCreate() {
        if (this.token != null) {
            window.location.href = "http://localhost:5173/";
        }
    },
    methods: {
        async handleSubmit(e) {
            e.preventDefault();
            const data = {
                username: this.username,
                password: this.password,
            };
            if (this.username === "" || this.password === "") {
                this.message =
                    "Không được để trống các trường tài khoản mật khẩu";
                return;
            } else {
                this.message = "";
                await axios
                    .post(link, data)
                    .catch((err) => {
                        console.log("Tài Khoản không tồn tại");
                        this.message = "Tài Khoản không tồn tại";
                        // this.error = err.response.data.data;
                    })
                    .then((res) => {
                        console.log(res);
                        localStorage.setItem("token", res.data.data);
                        // this.$store.dispatch('user', res.data.data);
                        // this.$router.push('/');
                        window.location.href = "http://localhost:5173/";
                    });
                // .catch((err) => {
                //     this.message = err.response.data.userMsg;
                // });
            }
        },
    },
};
</script>
