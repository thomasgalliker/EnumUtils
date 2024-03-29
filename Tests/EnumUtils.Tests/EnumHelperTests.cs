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
            weekday.Should().NotBe(null);
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
            parsed.Should().NotBe(null);
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
            action.Should().Throw<ArgumentException>().Which.Message.Should().Contain(enumString);
        }

        [Fact]
        public void ShouldTryParseWithSuccess()
        {
            // Arrange
            string enumString = "Fri";

            // Act
            var parsed = EnumHelper.TryParse<Weekday>(enumString);

            // Assert
            parsed.Should().NotBe(null);
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
            parsed.Should().NotBe(null);
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
            casted.Should().NotBe(null);
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
            casted.Should().NotBe(null);
            casted.Should().Be(Weekday.Mon);
        }

        [Fact]
        public void ShouldReturnRandomEnum()
        {
            // Arrange
            const int count = 1000;

            // Act
            var randomWeekdays = Enumerable.Range(0, count).Select(x => EnumHelper.GetRandom<Weekday>()).ToList();

            // Assert
            randomWeekdays.Should().HaveCount(count);
            randomWeekdays.Should().Contain(Weekday.Mon);
            randomWeekdays.Should().Contain(Weekday.Tue);
            randomWeekdays.Should().Contain(Weekday.Wed);
            randomWeekdays.Should().Contain(Weekday.Thu);
            randomWeekdays.Should().Contain(Weekday.Fri);
            randomWeekdays.Should().Contain(Weekday.Sat);
            randomWeekdays.Should().Contain(Weekday.Sun);
        }

        [Fact]
        public void ShouldReturnRandomEnum_ExcludingParticularValues()
        {
            // Arrange
            const int count = 1000;
            Weekday[] excludedValues = { Weekday.Sat, Weekday.Sun };

            // Act
            var randomWeekdays = Enumerable.Range(0, count).Select(x => EnumHelper.GetRandom(excludedValues)).ToList();

            // Assert
            randomWeekdays.Should().HaveCount(count);
            randomWeekdays.Should().Contain(Weekday.Mon);
            randomWeekdays.Should().Contain(Weekday.Tue);
            randomWeekdays.Should().Contain(Weekday.Wed);
            randomWeekdays.Should().Contain(Weekday.Thu);
            randomWeekdays.Should().Contain(Weekday.Fri);
            randomWeekdays.Should().NotContain(Weekday.Sat);
            randomWeekdays.Should().NotContain(Weekday.Sun);
        }

        [Fact]
        public void ShouldCountEnums()
        {
            // Act
            var count = EnumHelper.Count<Weekday>();

            // Assert
            count.Should().Be(7);
        }

#if NET452
        [Fact]
        public void ShouldGetDescriptions()
        {
            // Act
            var weekdayDescriptions = EnumHelper.GetDescriptions<Weekday>();

            // Assert
            weekdayDescriptions.Should().HaveCount(EnumHelper.Count<Weekday>());
        }
#endif
    }
}
