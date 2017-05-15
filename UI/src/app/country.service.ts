import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';

import { Observable } from 'rxjs/Rx';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';

import { Country } from 'app/models/country';

@Injectable()
export class CountryService {
  private agentsApi = 'api/v1/countries/';

  constructor(private http: Http) { }

  getAllCountries(): Observable<Country[]> {
    return this.http.get(this.agentsApi)
      .map(this.mapCountryCollection)
      .catch(this.handleError);
  }

  addCountry(country: Country): Observable<Country> {
    const headers = new Headers({ 'Content-Type': 'application/json' });
    const options = new RequestOptions({ headers: headers });

    return this.http.post(this.agentsApi, country, options)
      .map(this.mapCountry)
      .catch(this.handleError);
  }

  private mapCountry(res: Response) {
    const body = res.json();
    return new Country(body['id'], body['displayName'], body['isEnabled']) || {};
  }

  private mapCountryCollection(res: Response) {
    const body: any[] = res.json();
    const result = [];

    // tslint:disable-next-line:forin
    for (const i of body) {
      const country = new Country(i['id'], i['displayName'], i['isEnabled']);
      result.push(country);
    }
    return result;
  }

  private handleError(error: Response | any) {
    // In a real world app, you might use a remote logging infrastructure
    let errMsg: string;
    if (error instanceof Response) {
      const body = error.json() || '';
      const err = body.error || JSON.stringify(body);
      errMsg = `${error.status} - ${error.statusText || ''} ${err}`;
    } else {
      errMsg = error.message ? error.message : error.toString();
    }
    console.error(errMsg);
    return Observable.throw(errMsg);
  }
}
