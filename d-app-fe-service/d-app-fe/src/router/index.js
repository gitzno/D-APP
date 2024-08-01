import { createRouter, createWebHistory } from "vue-router";
import HomeView from "@/views/HomeView.vue";
import EditorView from "@/views/EditorView.vue";
import LoginView from "@/views/LoginView.vue";
import NotFound404 from "@/views/NotFound404.vue";
import Calendar from "@/views/Calendar.vue";
import register from '@/views/RegisterView.vue';
import reportView from "@/views/reportView.vue";
import FavoriteView from "@/views/FavoriteView.vue";

const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes: [
        {
            path: "/",
            name: "home",
            component: HomeView,
        },
        {
            path: "/Favorite",
            name: "favorite",
            component: FavoriteView,
        },
        {
            path: "/Home",
            redirect: "/"
        },
        {
            path: "/Homepage",
            redirect: "/"
        },
        {
            path: "/RichEditor/:id",
            name: "RichEditor",
            component: EditorView,
            props: true
        }, 
        {
             path: "/RichEditor",
            redirect: "/RichEditor/NewEditorForToday"
        },
        {
            path: "/report",
            name: "report",
            component: reportView,
        },
        {
            path: "/youCalendar",
            name: "youCalendar",
            component: Calendar,
        },
        {
            path: "/login",
            name: "Login",
            component: LoginView,
        },
        {
            path: "/register",
            name: "register",
            component: register,
        },
        {
            path: "/about",
            name: "about",
            // route level code-splitting
            // this generates a separate chunk (About.[hash].js) for this route
            // which is lazy-loaded when the route is visited.
            component: () => import("../views/AboutView.vue"),
        },
        {
            path: "/:catchAll(.*)",
            name: "NotFound",
            component: NotFound404,
            meta: {
                requiresAuth: false,
            },
        },
    ],
});

export default router;
