import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import VueRouter from 'vue-router';

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

interface SiteSearchResponse {
    sites: SiteViewModel[]
}

interface SiteViewModel {
    id: number,
    name: string,
    url: string,
    mode: string,
    website: string,
    database: string,
    purpose: string,
    company: string
}

@Component({
    components: {
        SiteListComponent: require('../sitelist/sitelist.vue.html')
    }
})
export default class SearchComponent extends Vue {
    $router: any;
    sites: SiteViewModel[] = [];

    searchViewModel: SearchViewModel = {
        sites: [{ id: 1, name: "Test", selected: true }],
        modes: [{ id: 1, name: "Test", selected: true }],
        databases: [{ id: 1, name: "Test", selected: true }],
        companies: [{ id: 1, name: "Test", selected: true }],
        purposes: [{ id: 1, name: "Test", selected: true }],
    };
    

    mounted() {
        fetch('api/search/GetOptions')
        .then(response => response.json() as Promise<SearchViewModel>)
        .then(data => {
            this.searchViewModel = data;
            console.log(this.searchViewModel);
        }).then(() => {
            fetch('api/search/getUrls', {
                method: 'POST', 
                body: JSON.stringify(this.searchViewModel), 
                headers: new Headers({
                  'Content-Type': 'application/json'
                })
              }).then(res => res.json()as Promise<SiteSearchResponse>)
              .then(returnedSites => {console.log(returnedSites); this.sites = returnedSites.sites; console.log(this.sites);} );

        });
    }

    search(e : any){
        e.preventDefault();    
        fetch('api/search/getUrls', {
            method: 'POST', 
            body: JSON.stringify(this.searchViewModel), 
            headers: new Headers({
              'Content-Type': 'application/json'
            })
          }).then(res => res.json()as Promise<SiteSearchResponse>)
          .then(returnedSites => {console.log(returnedSites); this.sites = returnedSites.sites; console.log(this.sites);} );

    }
}