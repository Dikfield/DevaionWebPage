import { Component, inject, OnInit } from '@angular/core';
import { TweetComponent } from '../tweet/tweet.component';
import { AccountService } from '../_services/account.service';
import { NgFor } from '@angular/common';
import { User } from '../_models/user';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [TweetComponent, NgFor],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class HomeComponent{
  accountService = inject(AccountService);
  tweetMode = false;

  tweetToggle() {
    this.tweetMode = !this.tweetMode;
  }
  cancelTweet(event: boolean) {
    this.tweetMode = event;
  }
}
