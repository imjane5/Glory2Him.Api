﻿// --------------------------------------------------------------------------------
// Copyright (c) Christo du Toit. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// FREE TO USE TO HELP SHARE THE GOSPEL
// Mark 16:15 NIV "Go into all the world and preach the gospel to all creation."
// https://mark.bible/mark-16-15 
// --------------------------------------------------------------------------------

using G2H.Api.Web.Models.PostComments;
using Microsoft.EntityFrameworkCore;

namespace G2H.Api.Web.Brokers.Storages
{
    public partial class StorageBroker
    {
        private static void AddPostCommentReferences(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostComment>()
                .HasKey(postComment =>
                    new
                    {
                        postComment.PostId,
                        postComment.CommentId
                    });

            modelBuilder.Entity<PostComment>()
                .HasOne(postComment => postComment.Post)
                .WithMany(post => post.PostComments)
                .HasForeignKey(postComment => postComment.PostId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PostComment>()
                .HasOne(postComment => postComment.Comment)
                .WithMany(attachment => attachment.PostComments)
                .HasForeignKey(postComment => postComment.CommentId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
