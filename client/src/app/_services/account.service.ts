import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { User } from '../_models/user';
import { map } from 'rxjs';
import { Tweet } from '../_models/tweet';
import { PostTweetDto } from '../_dtos/postTweetDto';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  private http = inject(HttpClient);
  // baseUrl =
  //   'https://xbots-djbmdta9hyeqc9ca.germanywestcentral-01.azurewebsites.net';
  baseUrl = environment.apiUrl;
  currentUser = signal<User | null>(null);

  login(model: any) {
    return this.http.post<User>(this.baseUrl + 'account/login', model).pipe(
      map((user) => {
        if (user) {
          this.setCurrentUser(user);
        }
        return user;
      })
    );
  }

  setCurrentUser(user: User) {
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUser.set(user);
  }

  getUsers() {
    return this.http.get(this.baseUrl + 'users', this.getHttpOptions());
  }

  tweet(model: PostTweetDto) {
    return this.http.post<Tweet>(
      this.baseUrl + 'tweet/posttweet',
      model,
      this.getHttpOptions()
    );
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUser.set(null);
  }

  getHttpOptions() {
    const token = this.currentUser()?.token; // Pegue o token do currentUser
    let headers = new HttpHeaders({
      Authorization: `Bearer ${token}`, // Adiciona o header Authorization
    });

    // Adiciona o header Content-Type, sem remover o Authorization
    headers = headers.set('Content-Type', 'application/json');

    return {
      headers: headers,
    };
  }
}
