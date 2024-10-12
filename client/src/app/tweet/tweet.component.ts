import { NgFor } from '@angular/common';
import { AccountService } from './../_services/account.service';
import { Component, inject, OnInit, output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { PostTweetDto } from '../_dtos/postTweetDto';

@Component({
  selector: 'app-tweet',
  standalone: true,
  imports: [FormsModule, NgFor],
  templateUrl: './tweet.component.html',
  styleUrl: './tweet.component.css',
})
export class TweetComponent implements OnInit {
  cancelTwitter = output<boolean>();
  private accountService = inject(AccountService);
  users: any = {};
  tweetDto: PostTweetDto = {
    title: '',
    question: '',
    language: '',
    characteres: 0,
    hashtags: 0,
  };

  ngOnInit(): void {
    this.loadUsers();
  }

  tweet() {
    this.accountService.tweet(this.tweetDto).subscribe({
      next: (response) => {
        console.log(response);
        this.cancel();
      },
      error: (error) => console.log(error),
    });
  }

  cancel() {
    this.cancelTwitter.emit(false);
  }

  loadUsers() {
    this.users = this.accountService.getUsers().subscribe({
      next: (users) => (this.users = users),
    });
  }
}
