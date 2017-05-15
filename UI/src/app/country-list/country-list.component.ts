import { Component, OnInit } from '@angular/core';
import { CountryService } from 'app/country.service';
import { Country } from 'app/models/country';

@Component({
  selector: 'app-country-list',
  templateUrl: './country-list.component.html',
  styleUrls: ['./country-list.component.css']
})
export class CountryListComponent implements OnInit {

  model: Country = new Country('', '', true);

  isEditingCountry: boolean;

  countries: Country[];

  errorMessage: string;

  constructor(private countryService: CountryService) {

  }

  newCountry() {
    this.isEditingCountry = false;
    this.model = new Country('', '', true);
  }

  saveCountry() {
    this.countryService.addCountry(this.model).subscribe(
      country => {
        this.countries.push(country);
        this.newCountry();
      },
      error => this.errorMessage = <any>error
    );
  }

  editCountry(country: Country) {
    this.isEditingCountry = true;
    this.model = country;
  }

  fetchCountries() {
    this.countryService.getAllCountries().subscribe(
      agents => this.countries = agents,
      error => this.errorMessage = <any>error
    );
  }

  ngOnInit() {
    this.fetchCountries();
    this.isEditingCountry = false;
  }

}
