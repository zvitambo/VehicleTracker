import { ToastyService } from "ng2-toasty";
import { ErrorHandler, Inject, NgZone } from "@angular/core";

export class AppErrorHandler implements ErrorHandler {
  constructor(
    //private NgZone: NgZone,
    @Inject(ToastyService) private ToastyService: ToastyService
  ) {}
  handleError(error: any): void {
    // this.NgZone.run(() => {
    //   console.log("ERROR");
    //   this.ToastyService.error({
    //     title: "Error",
    //     msg: "An unexpected error occured",
    //     theme: "bootstrap",
    //     showClose: true,
    //     timeout: 3000
    //   });
    // });
  }
}
