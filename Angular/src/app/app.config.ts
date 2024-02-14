import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { provideRouter } from '@angular/router';
import { HttpClientModule , HttpInterceptor, provideHttpClient, withInterceptors, withInterceptorsFromDi} from '@angular/common/http';
import { routes } from './app.routes';
import { Provider } from '@angular/core';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { authInterceptor } from './auth.interceptor';

// export const noopInterceptorProvider: Provider =
//   { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor};

export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routes),
  importProvidersFrom(HttpClientModule),
  provideHttpClient(
    withInterceptorsFromDi(),
    withInterceptors([ authInterceptor])
  ),
]};
