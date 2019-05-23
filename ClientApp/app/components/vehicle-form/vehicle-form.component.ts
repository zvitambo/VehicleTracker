import * as _ from 'underscore';
import { SaveVehicle , Vehicle } from './../../models/vehicle';
import { VehicleService } from "./../../services/vehicle.service";
import { Component, OnInit } from "@angular/core";
import { ToastyService } from "ng2-toasty";
import { ActivatedRoute, Route, Router } from "@angular/router";
import { Observable } from "rxjs/Observable";
import 'rxjs/add/observable/forkJoin';


@Component({
  selector: "app-vehicle-form",
  templateUrl: "./vehicle-form.component.html",
  styleUrls: ["./vehicle-form.component.css"]
})
export class VehicleFormComponent implements OnInit {
  // makes=[{id:''}];
  // models= MakeService[{id: ''}];
  makes: any[];
  models: any[];
  features: any[];
  vehicle: SaveVehicle = {
    id: 0,
    makeId: 0,
    modelId: 0,
    isRegistered: false,
    features: [],
    contact: {
      name: '',
      phone: '',
      email:''
    }
  };

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private vehicleService: VehicleService,
    private ToastyService: ToastyService
  ) {
    this.makes = [];
    this.models = [];
    route.params.subscribe(p => {
      this.vehicle.id = +p['id'];
    });
  }

  

  ngOnInit() {
    var sources = [
      this.vehicleService.getFeatures(),
      this.vehicleService.getMakes()
    ];

    if(this.vehicle.id)
    sources.push(this.vehicleService.getVehicle(this.vehicle.id));

    Observable.forkJoin(sources).subscribe(data => {
      this.features  = data[0];
      this.makes  = data[1];
      if(this.vehicle.id){
       this.setVehicle(data[2]);
       this.populateModels();
      }
    }, err => { if(err.status == 404) this.router.navigate(['/home'])
  } );
   
  }

  private setVehicle(v : Vehicle){

this.vehicle.id = v.id;
this.vehicle.makeId = v.make.id;
this.vehicle.modelId = v.model.id;
this.vehicle.isRegistered = v.isRegistered;
this.vehicle.contact = v.contact;
this.vehicle.features = _.pluck(v.features, 'id');
  }

  onMakeChange() { 
    this.populateModels();
    delete this.vehicle.modelId;
  }

  private populateModels(){
    var selectedMake = this.makes.find(m => m.id == this.vehicle.makeId);    
    this.models = selectedMake ? selectedMake.models : [];
  }

  onFeatureToggle(featureId, $event) {
    if ($event.target.checked) {
      this.vehicle.features.push(featureId);
    } else {
      var index = this.vehicle.features.indexOf(featureId);
      this.vehicle.features.splice(index, 1);
    }
  }

  submit() {

    if(this.vehicle.id){
      this.vehicleService.updateVehicle(this.vehicle).subscribe(x => {
          this.ToastyService.success({
        title: 'Success',
        msg: 'Vehicle Updated Successfully',
        theme: 'bootstrap',
        showClose: true,
        timeout: 3000
      });
      });
    }
    else {

      this.vehicleService.create(this.vehicle).subscribe(x => console.log(x));
    }
    
  }

  delete(){

    if(confirm('Are you sure '))
    this.vehicleService.deleteVehicle(this.vehicle.id).subscribe(x => {
      this.router.navigate(['/home']);
    });
  }

  onModelChange() {}
}




// this.vehicleService.getVehicle(this.vehicle.id).subscribe(v => {this.vehicle = v},
//   err => { if(err.status == 404) this.router.navigate(['/home'])}
//   );
//   this.vehicleService.getMakes().subscribe(makes => (this.makes = makes));
//   this.vehicleService
//     .getFeatures()
//     .subscribe(features => (this.features = features));