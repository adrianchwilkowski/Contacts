import { ChangeDetectorRef, Component, OnInit, computed, inject } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { NgIf } from '@angular/common';
import { IdentityService } from '../../services/identity.service';

@Component({
    selector: 'app-top-bar',
    templateUrl: './top-bar.component.html',
    styleUrls: ['./top-bar.component.css'],
    standalone: true,
    imports: [RouterLink, NgIf]
})
export class TopBarComponent implements OnInit{
    logged: boolean;
    constructor(private identityService: IdentityService,private cdRef: ChangeDetectorRef, private router: Router){
        this.logged = identityService.isLogged();
    }
    ngOnInit(): void {
        this.logged = this.identityService.isLogged();
    }
    onLogout() {
        this.identityService.logout();
        this.logged = false;
        this.router.navigate(['/']).then(() => {
            window.location.reload();
        });
    }
    onLogin(){
        this.router.navigate(['login']);
    }
}
