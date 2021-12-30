﻿// --------------------------------------------------------------------------------
// Copyright (c) Christo du Toit. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// FREE TO USE TO HELP SHARE THE GOSPEL
// Mark 16:15 NIV "Go into all the world and preach the gospel to all creation."
// https://mark.bible/mark-16-15 
// --------------------------------------------------------------------------------

using System.Linq;
using FluentAssertions;
using G2H.Api.Web.Models.Reactions;
using Moq;
using Xunit;

namespace G2H.Api.Web.Tests.Unit.Services.Foundations.Reactions
{
    public partial class ReactionServiceTests
    {
        [Fact]
        public void ShouldReturnReactions()
        {
            // given
            IQueryable<Reaction> randomReactions = CreateRandomReactions();
            IQueryable<Reaction> storageReactions = randomReactions;
            IQueryable<Reaction> expectedReactions = storageReactions;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllReactions())
                    .Returns(storageReactions);

            // when
            IQueryable<Reaction> actualReactions =
                this.reactionService.RetrieveAllReactions();

            // then
            actualReactions.Should().BeEquivalentTo(expectedReactions);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllReactions(),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
