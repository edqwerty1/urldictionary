import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import VueRouter from 'vue-router';
import { get, set, remove, getJSON } from 'js-cookie'

interface SearchViewModel {
    websites: SearchItem[];
    modes: SearchItem[];
    databases: SearchItem[];
    companies: SearchItem[];
    purposes: SearchItem[];
}

interface Cookie {
    websites: number[],
    modes: number[],
    databases: number[],
    companies: number[],
    purposes: number[],
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

interface ProfileViewModel {
    id: number,
    name: string,
    websites: SearchItem[];
    modes: SearchItem[];
    databases: SearchItem[];
    companies: SearchItem[];
    purposes: SearchItem[];
}

@Component({
    components: {
        SiteListComponent: require('../sitelist/sitelist.vue.html')
    }
})
export default class SearchComponent extends Vue {
    $router: any;
    sites: SiteViewModel[] = [];
    profiles: ProfileViewModel[] = [];
    profileName: string = "";

    searchViewModel: SearchViewModel = {
        websites: [{ id: 1, name: "Test", selected: true }],
        modes: [{ id: 1, name: "Test", selected: true }],
        databases: [{ id: 1, name: "Test", selected: true }],
        companies: [{ id: 1, name: "Test", selected: true }],
        purposes: [{ id: 1, name: "Test", selected: true }],
    };
    

    mounted() {
        fetch('api/search/GetProfiles')
        .then(response => response.json() as Promise<ProfileViewModel[]>)
        .then(data => {
            this.profiles = data;

        }).then(() => {

        fetch('api/search/GetOptions')
        .then(response => response.json() as Promise<SearchViewModel>)
        .then(data => {
            var profileUrl = this.$route.params.profileName;
            if (profileUrl){
                var profileId = this.profiles.filter(profile => profile.name == profileUrl)[0].id
                this.profileFilter(profileId);
            } else 
            if (get("search")){
                var cookie = getJSON("search")! as Cookie;
                data.companies.forEach(company => {company.selected = cookie.companies.indexOf(company.id) >= 0});
                data.databases.forEach(company => {company.selected = cookie.databases.indexOf(company.id) >= 0});
                data.websites.forEach(company => {company.selected = cookie.websites.indexOf(company.id) >= 0});
                data.modes.forEach(company => {company.selected = cookie.modes.indexOf(company.id) >= 0});
                data.purposes.forEach(company => {company.selected = cookie.purposes.indexOf(company.id) >= 0});

            }   
            this.searchViewModel = data;

        }).then(() => {
            fetch('api/search/getUrls', {
                method: 'POST', 
                body: JSON.stringify(this.searchViewModel), 
                headers: new Headers({
                  'Content-Type': 'application/json'
                })
              }).then(res => res.json()as Promise<SiteSearchResponse>)
              .then(returnedSites => {console.log(returnedSites); this.sites = returnedSites.sites; console.log(this.sites);} );

        });});
    }

    search(e : any){
        var cookie : Cookie = {
            companies: [],
            websites: [],
            modes: [],
            databases: [],
            purposes: [],
        }

        this.searchViewModel.companies.forEach(company => {
            if(company.selected){
                cookie.companies.push(company.id)
        }
    });
    this.searchViewModel.websites.forEach(company => {
        if(company.selected){
            cookie.websites.push(company.id)
    }
});
this.searchViewModel.modes.forEach(company => {
    if(company.selected){
        cookie.modes.push(company.id)
}
});
this.searchViewModel.databases.forEach(company => {
    if(company.selected){
        cookie.databases.push(company.id)
}
});
this.searchViewModel.purposes.forEach(company => {
    if(company.selected){
        cookie.purposes.push(company.id)
}
});
    set("search",JSON.stringify(cookie), { expires: 365 });

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

    saveProfile(){
var saveProfileModel : ProfileViewModel = {
    id: 0,
    name: this.profileName,
    companies: this.searchViewModel.companies.filter(company => company.selected),
    websites: this.searchViewModel.websites.filter(company => company.selected),
    modes: this.searchViewModel.modes.filter(company => company.selected),
    purposes: this.searchViewModel.purposes.filter(company => company.selected),
    databases: this.searchViewModel.databases.filter(company => company.selected),

}

        fetch('api/search/AddProfile', {
            method: 'POST', 
            body: JSON.stringify(saveProfileModel), 
            headers: new Headers({
              'Content-Type': 'application/json'
            })
          })
          .then(() => {
            fetch('api/search/GetProfiles')
            .then(response => response.json() as Promise<ProfileViewModel[]>)
            .then(data => {
                this.profiles = data;
    
            });}
          );

    }

    profileClick(e : any){
        this.profileFilter(e.target.value);
    }

    profileFilter(profileId: number){
        var profile = this.profiles.filter(prof => prof.id == profileId)[0];
        console.log(profile);
        this.searchViewModel.companies.forEach(company => {company.selected = profile.companies.filter(t => t.id == company.id).length > 0});
        this.searchViewModel.databases.forEach(company => {company.selected = profile.databases.filter(t => t.id == company.id).length > 0});
        this.searchViewModel.websites.forEach(company => {company.selected = profile.websites.filter(t => t.id == company.id).length >0});
        this.searchViewModel.modes.forEach(company => {company.selected = profile.modes.filter(t => t.id == company.id).length > 0});
        this.searchViewModel.purposes.forEach(company => {company.selected = profile.purposes.filter(t => t.id == company.id).length > 0});
        
    }
}