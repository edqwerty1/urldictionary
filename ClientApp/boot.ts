import './css/site.css';
import 'jquery';
import 'bootstrap';
import Vue from 'vue';
import VueRouter from 'vue-router';

// Tell Vue to use the plugin 
Vue.use(VueRouter);

const routes = [
    { path: '/', component: require('./components/search/search.vue.html') },
    { path: '/search', component: require('./components/search/search.vue.html') },
    { path: '/addsite', component: require('./components/addsite/addsite.vue.html') },
    { path: '/editsite/:id', component: require('./components/addsite/addsite.vue.html') },
];

new Vue({
    el: '#app-root',
    router: new VueRouter({ mode: 'history', routes: routes }),
    render: h => h(require('./components/app/app.vue.html'))
});
