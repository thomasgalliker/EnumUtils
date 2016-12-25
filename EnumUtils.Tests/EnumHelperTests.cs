using System;
using System.Collections.Generic;
using System.Linq;
using EnumUtils.Tests.Testdata;
using FluentAssertions;
using Xunit;

namespace EnumUtils.Tests
{
    public class EnumHelperTests
    {
        [Fact]
        public void ShouldReturnTrueIfTypeIsAnEnum()
        {
            // Arrange
            var weekday = Weekday.Mon;

            // Act
            bool isEnum = EnumHelper.IsEnum(weekday);

            // Assert
            isEnum.Should().BeTrue();
        }

        [Fact]
        public void ShouldReturnTrueIfTypeIsAnEnum_ExtensionMethod()
        {
            // Arrange
            var weekday = Weekday.Mon;

            // Act
            bool isEnum = weekday.IsEnum();

            // Assert
            isEnum.Should().BeTrue();
        }

        [Fact]
        public void ShouldReturnTrueIfTypeIsAnEnum_Generic()
        {
            // Act
            bool isEnum = EnumHelper.IsEnum<Weekday>();

            // Assert
            isEnum.Should().BeTrue();
        }

        [Fact]
        public void ShouldReturnTrueIfTypeIsAnEnum_Generic_ExtensionMethod()
        {
            // Arrange
            object weekday = Weekday.Mon;

            // Act
            bool isEnum = weekday.IsEnum();

            // Assert
            isEnum.Should().BeTrue();
        }

        [Fact]
        public void ShouldReturnTrueIfTypeIsANullableEnum()
        {
            // Act
            bool isEnum = EnumHelper.IsEnum<Weekday?>();

            // Assert
            isEnum.Should().BeTrue();
        }

        [Fact]
        public void ShouldReturnFalseIfTypeIsNotAnEnum()
        {
            // Act
            bool isEnum = EnumHelper.IsEnum<string>();

            // Assert
            isEnum.Should().BeFalse();
        }

        [Fact]
        public void ShouldGetValuesFromEnum()
        {
            // Act
            IEnumerable<Weekday> weekdays = EnumHelper.GetValues<Weekday>();

            // Assert
            weekdays.Should().NotBeNull();
            weekdays.Should().HaveCount(7);
        }

        [Fact]
        public void ShouldGetNameFromEnum()
        {
            // Act
            string weekday = EnumHelper.GetName(Weekday.Tue);

            // Assert
            weekday.Should().NotBeNull();
            weekday.Should().Be("Tue");
        }

        [Fact]
        public void ShouldGetNamesFromEnum()
        {
            // Act
            IEnumerable<string> weekdays = EnumHelper.GetNames<Weekday>();

            // Assert
            weekdays.Should().NotBeNull();
            weekdays.Should().HaveCount(7);
        }

        [Fact]
        public void ShouldGetValuesFromNullableEnum()
        {
            // Act
            IEnumerable<Weekday?> weekdays = EnumHelper.GetValues<Weekday?>();

            // Assert
            weekdays.Should().NotBeNull();
            weekdays.Should().HaveCount(7);
        }

        [Fact]
        public void ShouldGetRandom()
        {
            // Act
            var randomWeekdays = new List<Weekday>
            {
                EnumHelper.GetRandom<Weekday>(),
                EnumHelper.GetRandom<Weekday>(),
                EnumHelper.GetRandom<Weekday>(),
                EnumHelper.GetRandom<Weekday>(),
                EnumHelper.GetRandom<Weekday>(),
                EnumHelper.GetRandom<Weekday>(),
                EnumHelper.GetRandom<Weekday>(),
            };


            // Assert
            var weekdayGroups = randomWeekdays.GroupBy(s => s).Select(g => new
            {
                Weekday = g.Key,
                Count = g.Count()
            });

            foreach (var group in weekdayGroups)
            {
                group.Count.Should().BeLessThan(randomWeekdays.Count);
            }
        }

        [Fact]
        public void ShouldParseWithSuccess()
        {
            // Arrange
            string enumString = "Thu";

            // Act
            var parsed = EnumHelper.Parse<Weekday>(enumString);

            // Assert
            parsed.Should().NotBeNull();
            parsed.Should().Be(Weekday.Thu);
        }

        [Fact]
        public void ShouldParseWithFailure()
        {
            // Arrange
            string enumString = "non-existent-weekday";

            // Act
            Action action = () => EnumHelper.Parse<Weekday>(enumString);

            // Assert
            action.ShouldThrow<ArgumentException>().Which.Message.Should().Contain("Requested value \'" + enumString + "\' was not found.");
        }

        [Fact]
        public void ShouldTryParseWithSuccess()
        {
            // Arrange
            string enumString = "Fri";

            // Act
            var parsed = EnumHelper.TryParse<Weekday>(enumString);

            // Assert
            parsed.Should().NotBeNull();
            parsed.Should().Be(Weekday.Fri);
        }

        [Fact]
        public void ShouldTryParseWithFailure()
        {
            // Arrange
            string enumString = "non-existent-weekday";

            // Act
            var parsed = EnumHelper.TryParse<Weekday>(enumString);

            // Assert
            parsed.Should().NotBeNull();
            parsed.Should().Be(default(Weekday));
        }

        [Fact]
        public void ShouldCastToEnum()
        {
            // Arrange
            int enumValue = 3;

            // Act
            var casted = EnumHelper.Cast<Weekday>(enumValue);

            // Assert
            casted.Should().NotBeNull();
            casted.Should().Be(Weekday.Wed);
        }

        [Fact]
        public void ShouldCastToEnumWithDefaultValue()
        {
            // Arrange
            int enumValue = 33;

            // Act
            var casted = EnumHelper.Cast(enumValue, defaultValue: Weekday.Mon);

            // Assert
            casted.Should().NotBeNull();
            casted.Should().Be(Weekday.Mon);
        }
    }
}
