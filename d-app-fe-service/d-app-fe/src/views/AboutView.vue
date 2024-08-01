<template lang="">
    <div class="bg-card">
        <div class="card">
            <div class="login">
                <h2 class="login-header">Thay đổi thông tin người dùng</h2>
                <form @submit="handleSubmit" class="login-container">
                    <p>
                        <input
                            v-model="this.res.userFname"
                            type="text"
                            placeholder="Họ"
                            required
                        />
                    </p>
                    <p>
                        <input
                            v-model="this.res.userLname"
                            type="text"
                            placeholder="Tên"
                            required
                        />
                    </p>
                    <p>
                        <input
                            v-model="this.res.userEmail"
                            type="text"
                            placeholder="Email"
                            required
                        />
                    </p>

                    <p>
                        <input
                            v-model="this.res.userPhoneNumber"
                            type="text"
                            placeholder="Số điện thoại"
                            required
                        />
                    </p>
                    <p><input type="submit" value="Cập Nhật" /></p>
                </form>
            </div>
        </div>
    </div>
</template>
<script>
import axios from "axios";
export default {
    data() {
        return {
            res: {},
        };
    },
    props: ["token"],
    async beforeCreate() {
        const response = await axios
            .get("http://localhost:5217/api/Users/Profile", {
                headers: {
                    Authorization: "Bearer " + this.token,
                },
            })
            .catch((err) => {
                console.log(err);
            });
        console.log(response);
        this.res = response.data.data;
    },
    methods: {
        convertDate(dateTimeString) {
            const date = new Date(dateTimeString);

            // Get the year, month, and day
            const year = date.getFullYear();
            const month = String(date.getMonth() + 1).padStart(2, "0"); // Months are zero-based, so add 1 and pad with leading zero if needed
            const day = String(date.getDate()).padStart(2, "0"); // Pad day with leading zero if needed

            // Format the date as YYYY-MM-DD
            const formattedDate = `${year}-${month}-${day}`;

            return formattedDate;
        },

        async handleSubmit(e) {
            e.preventDefault();

            const data = {
                userFname: this.res.userFname,
                userLname: this.res.userLname,
                userEmail: this.res.userEmail,
                userName: this.res.userName,
                userDateOfBirth: this.convertDate(this.res.userDateOfBirth),
                userPhoneNumber: this.res.userPhoneNumber,
            };
            console.log(data);
            this.error = {};
            this.message = "";
            const respone = await axios
                .put("http://localhost:5217/api/Users/UpdateProfile", data, {
                    headers: {
                        Authorization:
                            "Bearer " + localStorage.getItem("token"),
                    },
                })
                .catch((err) => {
                    console.log(err);
                });
            console.log(respone);
            if (respone.status == 200) {
                alert(respone.data.userMsg);
                this.$router.push("/");
                return;
            }
        },
    },
};
</script>
<style>
@media (min-width: 1024px) {
    .about {
        min-height: 100vh;
        display: flex;
        align-items: center;
    }
}
</style>
