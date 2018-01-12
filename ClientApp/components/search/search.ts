import Vue from 'vue';
import { Component } from 'vue-property-decorator';

interface SearchViewModel {
    sites: SearchItem[];
    modes: SearchItem[];
    databases: SearchItem[];
    companies: SearchItem[];
    purposes: SearchItem[];
}

interface SearchItem {
    id: number;
    name: string;
    selected: boolean;
}

@Component
export default class SearchComponent extends Vue {
    searchViewModel: SearchViewModel = {
        sites: [{ id: 1, name: "Test", selected: true }],
        modes: [{ id: 1, name: "Test", selected: true }],
        databases: [{ id: 1, name: "Test", selected: true }],
        companies: [{ id: 1, name: "Test", selected: true }],
        purposes: [{ id: 1, name: "Test", selected: true }],
    };

    mounted() {

    }
}