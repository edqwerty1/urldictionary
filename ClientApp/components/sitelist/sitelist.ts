import Vue from 'vue';
import { Component, Inject, Model, Prop, Provide, Watch } from 'vue-property-decorator'
interface SiteViewModel {
    id: number,
    name: string,
    url: string
}

@Component
export default class SiteListComponent extends Vue {
    @Prop() sites: SiteViewModel[] = [];// = [{id:1, name: "test", url:"http://www.google.com"}, {id:1, name: "test", url:"www.google.com"}]
    mounted() {

    }
}