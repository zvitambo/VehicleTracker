import { Injectable } from "@angular/core";
import { Http } from "@angular/http";
import "rxjs/add/operator/map";

@Injectable()
export class VehicleService {
  private http: Http;
  private readonly vehiclesEndPoint = '/api/vehicle/'; 
 
  constructor(http: Http) {
    this.http = http;
  }

  getFeatures() {
    return this.http.get("features").map(res => res.json());
  }

  getMakes() {
    return this.http.get(this.vehiclesEndPoint + "makes").map(res => res.json());
  }

  getModels() {
    return this.http.get(this.vehiclesEndPoint + "models").map(res => res.json());
  }


  create(vehicle) {
    return this.http
      .post(this.vehiclesEndPoint + "createVehicle", vehicle)
      .map(res => res.json());
  }
  getVehicle(id) {
    return this.http
      .get(this.vehiclesEndPoint + "GetVehicle/" + id)
      .map(res => res.json());
  }
  getVehicles(filter) {
    return this.http.get(this.vehiclesEndPoint + "GetVehicles/" + '?' + this.toQueryString(filter)).map(res => res.json());
  }

  toQueryString(obj){
   var parts: any[] = [];
   //var parts =[];
 
   for(var property  in obj){
     var value = obj[property];

     if(value != null && value != undefined)  {
      parts.push(encodeURIComponent(property) + '=' + encodeURIComponent(value));  
      // const prop: string = encodeURIComponent(property); 
      // const val: string = encodeURIComponent(value);  
      // parts.push(prop + '=' + val);   
     }   
        
   }

   return parts.join('&');
  }

  updateVehicle(vehicle) {
    return this.http
      .put(this.vehiclesEndPoint + "UpdateVehicle/" + vehicle.id, vehicle)
      .map(res => res.json());
  }
  deleteVehicle(id) {
    return this.http
      .delete(this.vehiclesEndPoint + "DeleteVehicle/" + id)
      .map(res => res.json());
  }
}
