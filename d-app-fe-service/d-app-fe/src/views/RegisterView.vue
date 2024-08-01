<script setup>
import router from "@/router";
import axios from "axios";
</script>
<template lang="">
    <div class="bg-card">
        <div class="card">
            <div class="login">
                <h2 class="login-header">Đăng Ký Ngay</h2>
                <p style="color: red; text-align: center">{{ message }}</p>
                <form @submit="handleSubmit" class="login-container">
                    <p>
                        <input
                            v-model="userFname"
                            type="text"
                            placeholder="Tên"
                            required
                        />      
                        <p style="color: red; text-align: center" v-if="!(error.userFname == userFname)">{{ error.userFname }}</p>
                    </p>
                    <p>
                        <input
                            v-model="userLname"
                            type="text"
                            placeholder="Họ"
                            required
                        />
                        <p style="color: red; text-align: center"  v-if="!(error.userLname == userLname)">{{ error.userLname }}</p>
                    </p>
                    <p>
                        <input
                            v-model="userEmail"
                            type="email"
                            placeholder="Email"
                            required
                        />
                        
                        <p style="color: red; text-align: center" v-if="!(error.userEmail == userEmail)">{{ error.userEmail }}</p>
                    </p>
                    <p>
                        <input
                            v-model="userName"
                            type="text"
                            placeholder="Tên đăng nhập"
                            required
                        />
                        
                        <p style="color: red; text-align: center"v-if="!(error.userName == userName)">{{ error.userName }}</p>
                    </p>
                    <p>
                        <input v-model="userDateOfBirth" type="date" 
                        required/>
                    </p>
                    <p>
                        <input
                            v-model="userPhoneNumber"
                            type="text"
                            placeholder="Số điện thoại"
                            required
                        />
                        
                        <p style="color: red; text-align: center" v-if="!(error.userPhoneNumber == userPhoneNumber)">{{ error.userPhoneNumber }}</p>
                    </p>
                    <p>
                        <input
                            v-model="userPassword"
                            type="password"
                            placeholder="Mật Khẩu"
                            required
                        />
                        
                        <p style="color: red; text-align: center" v-if="!(error.userPassword == userPassword)">{{ error.userPassword }}</p>
                    </p>
                    <p><input type="submit" value="Đăng Ký" /></p>
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
const link = "http://localhost:5217/api/Users/Register";
export default {
    name: "login",
    data() {
        return {
            error: {},
            message: "",
            userFname: "",
            userLname: "",
            userEmail: "",
            userName: "",
            userDateOfBirth: "",
            userPhoneNumber: "",
            userPassword: "",
        };
    }, props: ["token"],
    
    async beforeCreate() {
        if(this.token != null){
            window.location.href = 'http://localhost:5173/';
        }
    },
    methods: {
        async handleSubmit(e) {
            e.preventDefault();
            
            const data = {
                userFname: this.userFname,
                userLname: this.userLname,
                userEmail: this.userEmail,
                userName: this.userName,
                userDateOfBirth: this.userDateOfBirth,
                userPhoneNumber: this.userPhoneNumber,
                userPassword: this.userPassword,
            };
            console.log(data);
            this.error = {};
            this.message = "";
            const respone = await axios.post(link, data).catch((err) => {
                    console.log(err)
                    this.message = err.response.data.userMsg;
                    this.error = err.response.data.data;
                });;
            console.log(respone);
            if(respone.status == 200){
                alert(respone.data.userMsg);
                router.push('/login');
                return;
            }                
        },
    },
};
</script>
