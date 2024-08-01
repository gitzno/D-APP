<template>
    <div>
        <div class="box">
            <input
                v-model="object.linkImg"
                type="name"
                placeholder=""
                id="name"
            />

            <label for="name">link img</label>
        </div>
        <form style="display: flex" @submit.prevent="submitForm">
            <div class="box">
                <input
                    v-model="object.docTitle"
                    type="name"
                    placeholder=""
                    id="name"
                />

                <label for="name">Title</label>
            </div>
            <div style="display: flex; align-items: center">
                <input
                    style="margin: 10px"
                    v-model="this.object.docStatus"
                    type="checkbox"
                    placeholder=""
                    id="name"
                />
                EVENT
            </div>
            <div v-on:click="btnHandle()" class="buttonzzz">Save</div>
            <div
                v-on:click="btnHandleDelete()"
                class="buttonzzz"
                style="background-color: red"
            >
                Delete
            </div>
        </form>
    </div>
    Lần Chỉnh Sửa Cuối: {{ formatDateTime(object.docModifiDate) }}<br />
    <div class="box boxs">
        <textarea
            v-model="object.docCont"
            type="name"
            placeholder="Viết Nhật ký của bạn"
            id="name"
        />
    </div>
    <!-- <MyEditor v-model="contentND" /> -->
    <!-- {{ this.contentND }} -->
    <!-- <EditorContent :editor="editor" /> -->
</template>
<script setup>
import axios from "axios";
</script>
<script>
export default {
    methods: {
        async btnHandleDelete() {
            const dataz = this.id;
            // console.log("chuẩn bị xóa id:" + data);
            console.log("Bearer " + localStorage.getItem("token"));
            const response = await axios.delete(
                `http://localhost:5217/api/Document/Delete?docId=${dataz}`, // `data` should be passed as the second parameter
                {
                    headers: {
                        Authorization:
                            "Bearer " + localStorage.getItem("token"),
                    },
                }
            );
            alert(response.data.userMsg);
            this.$router.push("/");
        },
        async btnHandle() {
            console.log("update document:" + this.object.docStatus);
            if (
                this.object.docTitle === this.objectOldd &&
                this.object.docCont === this.objectOldz
            ) {
                alert("Bạn cần thay đổi title hoặc conent của nhật ký!");
                return;
            }

            const data = {
                docTitle: this.object.docTitle,
                docCont: this.object.docCont,
                docStatus: this.object.docStatus ? 1 : 0,
                linkImg: this.object.linkImg,
            };
            let idz = this.id == "NewEditorForToday" ? this.idFake : this.id;
            const response = await axios.post(
                `http://localhost:5217/api/Document/updateDocument?docid=${idz}`,
                data, // `data` should be passed as the second parameter
                {
                    headers: {
                        Authorization:
                            "Bearer " + localStorage.getItem("token"),
                    },
                }
            );
            alert(response.data.userMsg);
            this.$router.push("/");
            console.log(response);
        },

        formatDateTime(dateString) {
            if (dateString == undefined) {
                const date = new Date();

                const day = String(date.getDate()).padStart(2, "0");
                const month = String(date.getMonth() + 1).padStart(2, "0"); // Tháng trong JavaScript bắt đầu từ 0, nên cần cộng thêm 1
                const year = date.getFullYear();
                const hours = String(date.getHours()).padStart(2, "0");
                const minutes = String(date.getMinutes()).padStart(2, "0");

                const formattedDate = `${day}/${month}/${year} lúc ${hours}:${minutes}`;

                return formattedDate;
            }
            const date = new Date(dateString);

            const day = date.getDate().toString().padStart(2, "0");
            const month = (date.getMonth() + 1).toString().padStart(2, "0");
            const year = date.getFullYear();
            const hours = date.getHours().toString().padStart(2, "0");
            const minutes = date.getMinutes().toString().padStart(2, "0");

            return `${day}/${month}/${year} lúc ${hours}:${minutes}`;
        },
    },
    props: ["token", "id"],
    data() {
        return {
            // editor: null,
            object: {},
            editor: null,
            idFake: "",
            objectOldz: "",
            objectOldd: "",
        };
    },
    async beforeCreate() {
        if (this.id == "NewEditorForToday") {
            const response = await axios
                .get("http://localhost:5217/api/Document/newDocument", {
                    headers: {
                        Authorization: "Bearer " + this.token,
                    },
                })
                .catch((err) => {
                    console.log(err);
                });
            console.log(response.data.data);
            console.log(this.id);
            this.idFake = response.data.data;
            //  this.id = response.data.data;
            // this.contentND = await this.object.docCont;
            return;
        }

        const response = await axios
            .get(
                "http://localhost:5217/api/Document/getDocument?docId=" +
                    this.id,
                {
                    headers: {
                        Authorization: "Bearer " + this.token,
                    },
                }
            )
            .catch((err) => {
                console.log(err);
            });
         console.log(response.data.data);
        this.object = response.data.data;
        
        this.object.docStatus = this.object.docStatus == 1;
        this.objectOldz = response.data.data.docCont;
        this.objectOldd = response.data.data.docTitle;
        this.contentND = await this.object.docCont;
    },
};
</script>
<style lang="scss" scope>
.buttonzzz {
    cursor: pointer;
    color: white;
    background-color: black;
    text-decoration: none;
    padding: 15px 30px;
    border-radius: 10px;
    margin: 20px;
    transition: background-color 0.5s;
}
.boxs {
    width: 100%;
    textarea {
        width: 100%;
        height: 200px;
        padding: 20px;
        margin: 0;
        box-sizing: border-box;
        overflow: hidden;
        resize: none;
    }
}
.box {
    display: inline-flex;
    flex-direction: column;
    position: relative;
    font-family: Poppins;
    box-shadow: 0 0 3px #ddd;
    overflow: hidden;
    margin: 20px 0 20px 0;
    border-radius: 5px;
    background: #fff;
    label {
        position: absolute;
        display: flex;
        justify-content: flex-start;
        align-items: center;
        padding: 0 0 0 10px;
        width: 100%;
        height: 100%;
        color: #bbb;
        user-select: none;
        transition: 0.5s;
    }
    input {
        min-width: 800px;
        font-family: Poppins;
        padding: 20px 30px 5px 10px;
        border: none;
        outline: none;
        border-bottom: 3px solid #ccc;
        border-radius: 5px;
        transition: 0.5s;
        &:focus + label,
        &:not(:placeholder-shown) + label {
            transform: translateY(-30%);
            font-size: 0.6rem;
        }
        &:not(:placeholder-shown) {
            border-bottom: 3px solid #00bb00;
        }
    }
}

button {
    width: 50%;
    align-self: flex-end;
    padding: 10px 5px;
    border-radius: 0.25rem;
    font-weight: 600;
    letter-spacing: 3px;
    background: #0b0;
    color: #fff;
    border: none;
    transition: 0.5s;
    cursor: pointer;
    &:hover {
        background: #005500;
        letter-spacing: 0px;
    }
    &:active {
        transform: scale(1.1);
    }
}

.tiptap {
    background-color: #fff;
    box-shadow: 0 1px rgba(0, 0, 0, 0.08);
    border-radius: 20px;
    min-height: 100vh;
    outline: none !important;
    padding-top: 30px;
    padding-left: 30px;
}
.tiptap p.is-editor-empty:first-child::before {
    width: 500px;
    color: #adb5bd;
    content: attr(data-placeholder);
    float: left;
    height: 0;
    pointer-events: none;
}
</style>
