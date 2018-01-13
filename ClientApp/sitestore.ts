interface SiteViewModel {
    id: number,
    name: string,
    url: string
}

export class SiteStore {
    sites: SiteViewModel[];

    addSite(site: SiteViewModel){
        this.sites.push(site);
    }

    fetch() : SiteViewModel[]{
        return this.sites;
    }

    initialise(){
        
    }
}