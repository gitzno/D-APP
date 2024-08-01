<template lang="">
    <div style="width: 1280px">
        <div v-if="token == null">
            <div class="welcome">Welcome to D-APP</div>
        </div>
        <div style="display: block" v-if="token != null" class="homepage">
            <h1>Nhật ký bạn yêu thích:</h1>

            <div style="display: flex; flex-wrap: wrap">
                <div v-for="item in list" class="card-main">
                    <div class="card-img">
                        <img
                            v-if="item.linkImg == ''"
                            src="https://i.ibb.co/vhKYByB/cardimg.jpg"
                            alt=""
                        /><img
                            v-if="item.linkImg != ''"
                            :src="item.linkImg"
                            alt=""
                        />
                    </div>
                    <router-link
                        :to="{
                            name: 'RichEditor',
                            params: { id: item.docId },
                        }"
                        style="text-decoration: none; color: black"
                        class="card-content"
                    >
                        <h3
                            class="card-title"
                            style="
                                white-space: nowrap;
                                overflow: hidden;
                                text-overflow: ellipsis;
                                width: 200px;
                            "
                        >
                            {{ item.docTitle }}
                            <div style="color: red" v-if="item.docStatus != 0">
                                (EVENT)
                            </div>
                        </h3>
                        <p
                            class="card-description"
                            style="
                                white-space: nowrap;
                                overflow: hidden;
                                text-overflow: ellipsis;
                                width: 200px;
                            "
                        >
                            {{
                                item.docCont == ""
                                    ? "Nhật ký này chưa có nội dung"
                                    : item.docCont
                            }}
                        </p>
                        <div class="card-date">
                            <p class="date">
                                Created:
                                {{ formatDateTime(item.docCreateDate) }}
                            </p>
                            <p class="date">
                                Modified:
                                {{ formatDateTime(item.docModifiDate) }}
                            </p>
                        </div>
                    </router-link>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
import axios from "axios";
export default {
    data() {
        return {
            list: [],
        };
    },
    props: ["token"],
    methods: {
        formatDateTime(dateString) {
            const date = new Date(dateString);

            // Get the month, date, year, hours, minutes, and seconds
            const month = (date.getMonth() + 1).toString().padStart(2, "0");
            const day = date.getDate().toString().padStart(2, "0");
            const year = date.getFullYear();
            const hours = date.getHours().toString().padStart(2, "0");
            const minutes = date.getMinutes().toString().padStart(2, "0");
            const seconds = date.getSeconds().toString().padStart(2, "0");

            // Format the date and time as MM/dd/YYYY HH:mm:ss
            return `${month}/${day}/${year} ${hours}:${minutes}:${seconds}`;
        },
    },
    async created() {
        console.log(this.token);
        const response = await axios.get(
            "http://localhost:5217/api/Document/getAllDocByUserFavorite",
            {
                headers: {
                    Authorization: "Bearer " + this.token,
                },
            }
        );
        this.list = response.data.data;
        this.list = this.list.sort(
            (a, b) => new Date(b.docModifiDate) - new Date(a.docModifiDate)
        );
        console.log(response.data.data);
    },
};
</script>
<style scoped>
.card-main:hover {
    cursor: pointer;
}
.card-main:hover .card-title {
    color: blue;
}
.homepage {
    /* background-color: aqua; */
    min-height: 100vh;
}
.homepage h1 {
    padding-top: 20px;
}
.welcome {
    display: flex;
    align-items: center;
    justify-content: center;
    /* background-color: aqua; */
    font-weight: bold;
    font-size: 100px;
    height: 50vh;
}

/* //////////////////////// */
.card-container {
    display: flex;
    justify-content: center;
    align-items: center;
    height: 100vh;
    background-color: #f5f5f5;
}

.card-main {
    margin: 10px;
    display: flex;
    width: 80%;
    max-width: 600px;
    background-color: #fff;
    box-shadow: 0 0 20px rgba(0, 0, 0, 0.1);
    border-radius: 10px;
    overflow: hidden;
}

.card-img {
    flex-basis: 40%;
}

.card-img img {
    width: 100%;
    height: 100%;
    object-fit: cover;
}

.card-content {
    flex-basis: 60%;
    padding: 40px;
}

.card-title {
    font-size: 24px;
    font-weight: bold;
    margin-bottom: 20px;
}

.card-description {
    font-size: 16px;
    line-height: 1.5;
    margin-bottom: 30px;
}

.card-date {
    display: flex;
    justify-content: space-between;
    font-size: 14px;
    color: #888;
}

.date {
    margin: 0;
}
</style>
