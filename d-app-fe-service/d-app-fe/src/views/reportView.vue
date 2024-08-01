<template lang="">
    <div>
        <div>
            <div>
                <input type="text" v-model="this.searchtxt" />
                <input @click="myFunction" type="button" value="search" />
            </div>
            <div>
                <input
                    @click="searchAndSortByDocTitle"
                    type="button"
                    value="sort"
                />
            </div>
            <div>
                <select v-model="pageSize">
                    <option>5</option>
                    <option>10</option>
                    <option>15</option>
                    <option>20</option>
                </select>
                <!-- <input @click="console.log(Math.ceil(this.list.length/this.pageSize))" type="button" /> -->
                <div style="display: flex" >
                    <input v-for="(item, index) in Math.ceil(this.constList.length/this.pageSize)" @click="paging(this.pageSize, index + 1)" type="button" :value="index + 1" />
                </div>
            </div>
        </div>
        <table>
            <tr>
                <th>STT</th>
                <th>Title</th>
                <th>Modified Date</th>
                <th>Created Date</th>
            </tr>
            <tr v-for="(item, index) in list">
                <td>
                    <router-link
                        :to="{
                            name: 'RichEditor',
                            params: { id: item.docId },
                        }"
                        style="text-decoration: none; color: black"
                        class="card-content"
                        >{{ index + 1 }}</router-link
                    >
                </td>
                <td>
                    <router-link
                        :to="{
                            name: 'RichEditor',
                            params: { id: item.docId },
                        }"
                        style="text-decoration: none; color: black"
                        class="card-content"
                        >{{ item.docTitle }}</router-link
                    >
                </td>
                <td>
                    <router-link
                        :to="{
                            name: 'RichEditor',
                            params: { id: item.docId },
                        }"
                        style="text-decoration: none; color: black"
                        class="card-content"
                        >{{
                            this.formatDateTime(item.docModifiDate)
                        }}</router-link
                    >
                </td>
                <td>
                    <router-link
                        :to="{
                            name: 'RichEditor',
                            params: { id: item.docId },
                        }"
                        style="text-decoration: none; color: black"
                        class="card-content"
                        >{{
                            this.formatDateTime(item.docCreateDate)
                        }}</router-link
                    >
                </td>
            </tr>
        </table>
    </div>
</template>
<script>
import axios from "axios";
export default {
    props: ["token"],
    data() {
        return {
            constList: [],
            list: [],
            searchtxt: "",
            sortOrderAsc: true,
            pageSize: 10,
            // Biến để theo dõi thứ tự sắp xếp
        };
    },
    methods: {
        paging(pageSize, pageNumber) {
            const startIndex = (pageNumber - 1) * pageSize;
            const endIndex = startIndex + pageSize;
            this.list = this.constList.slice(startIndex, endIndex);
        },
        searchAndSortByDocTitle() {
            // Sắp xếp filteredList theo trường docTitle
            this.list = this.list.sort((a, b) => {
                const titleA = a.docTitle.toLowerCase();
                const titleB = b.docTitle.toLowerCase();

                if (titleA < titleB) return this.sortOrderAsc ? -1 : 1;
                if (titleA > titleB) return this.sortOrderAsc ? 1 : -1;
            });

            // Đảo ngược thứ tự sắp xếp cho lần gọi tiếp theo
            this.sortOrderAsc = !this.sortOrderAsc;
        },

        myFunction() {
            if (this.searchtxt == "") {
                this.loadData();
                return;
            }
            const lowerCaseSearchString = this.searchtxt.toLowerCase();

            // Sử dụng phương thức filter để lọc ra các đối tượng có docTitle chứa searchString không phân biệt chữ hoa chữ thường
            this.list = this.constList.filter((item) =>
                item.docTitle.toLowerCase().includes(lowerCaseSearchString)
            );
            console.log(this.searchtxt);
        },
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
        async loadData() {
            console.log(this.token);
            const response = await axios.get(
                "http://localhost:5217/api/Document/getAllDocByUser",
                {
                    headers: {
                        Authorization: "Bearer " + this.token,
                    },
                }
            );
            this.list = response.data.data;
            this.constList = response.data.data;
            
            console.log(response.data.data);
        },
    },
    async created() {
        this.loadData();
    },
};
</script>
<style scoped>
table {
    font-family: arial, sans-serif;
    border-collapse: collapse;
    width: 100%;
}

td,
th {
    border: 1px solid #dddddd;
    text-align: left;
    padding: 8px;
}

tr:nth-child(even) {
    background-color: #dddddd;
}
</style>
