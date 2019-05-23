import { Vehicle, KeyValuePair } from './../../models/vehicle';
import { VehicleService } from './../../services/vehicle.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-vehicle-list',
  templateUrl: './vehicle-list.component.html',
  styleUrls: ['./vehicle-list.component.css']
})
export class VehicleListComponent implements OnInit {
  vehicles: Vehicle[];
  //allVehicles: Vehicle[];
  makes: KeyValuePair[];
  models: KeyValuePair[];
  filter: any = {};
  private VehicleService;
  constructor(VehicleService : VehicleService) { 
  this.VehicleService = VehicleService;
  }

  ngOnInit() {
    this.VehicleService.getMakes().subscribe(makes => this.makes = makes);
    this.VehicleService.getModels().subscribe(models => this.models = models);
    console.log(this.makes);
    this.populateVehicles();
    //this.VehicleService.getVehicles(this.filter).subscribe(vehicles => this.vehicles = this.allVehicles = vehicles );
  }
populateVehicles (){
  this.VehicleService.getVehicles(this.filter).subscribe(vehicles => this.vehicles = vehicles );
}

  onFilterChange() {
    this.populateVehicles();
  }

  resetFilter() {
    this.filter = {};
    this.onFilterChange();
  }

sortBy(sortStr){

  if (this.filter.sortBy === sortStr){
    this.filter.isSortAscending = false;
  }else {
  this.filter.sortBy = sortStr;
  this.filter.isSortAscending = true;
  }
  this.populateVehicles();
}

}


// onFilterChange() {
//   var vehicles = this.allVehicles;
//    if(this.filter.makeId)
//       vehicles = vehicles.filter(v => v.make.id == this.filter.makeId);

//       this.vehicles = vehicles;
     
// }