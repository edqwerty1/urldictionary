import Vue from 'vue';
import { Component } from 'vue-property-decorator';

interface addSiteModel {
    id : number,
    name : string,
    url : string,
    selectedPurpose : number,
    selectedCompany: number,
    selectedDatabase : number,
    selectedMode: number,
    selectedWebsite : number,
    purposes: ListItem[],
    companies: ListItem[],
    databases: ListItem[],
    modes: ListItem[],
    websites: ListItem[]
}

interface ListItem {
    id: number;
    name: string;
}

@Component
export default class AddSiteComponent extends Vue {
    searchViewModel: addSiteModel = {
        id: 1,
        name: "test",
        url: 'www.google.com',
        selectedPurpose: 2,
        selectedCompany: 2,
        selectedDatabase: 3,
        selectedMode: 1,
        selectedWebsite: 2,
        purposes: [{id: 1, name: "one"}, {id: 2, name: "two"}, {id: 3, name: "three"}],
        companies: [{id: 1, name: "one"}, {id: 2, name: "two"}, {id: 3, name: "three"}],
        databases: [{id: 1, name: "one"}, {id: 2, name: "two"}, {id: 3, name: "three"}],
        modes: [{id: 1, name: "one"}, {id: 2, name: "two"}, {id: 3, name: "three"}],
        websites: [{id: 1, name: "one"}, {id: 2, name: "two"}, {id: 3, name: "three"}]
    };

    mounted() {

    }

    addSite(e : any){
        e.preventDefault();    
        fetch('api/url/AddUrl', {
            method: 'POST', 
            body: JSON.stringify(this.searchViewModel), 
            headers: new Headers({
              'Content-Type': 'application/json'
            })
          }).then(res => res.json())
          .catch(error => console.error('Error:', error))
          .then(response => {
              console.log('Success:', response);
              this.$router.push('search');}
            );
    }
}