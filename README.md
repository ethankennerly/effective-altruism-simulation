# Poor Family Simulator

How can I help?

Some charities are 1000 times more effective.

The following charities might be effective:

- [GiveDirectly](https://www.givewell.org/charities/give-directly)
- [Against Malaria Foundation](https://www.givewell.org/charities/amf)
- [Deworm the World](https://www.givewell.org/charities/deworm-world-initiative)
- [Pratham](https://www.givewell.org/international/charities/pratham)

[GiveWell](https://www.givewell.org) reviewed the above charities. 

Let's simulate these charities effects' on a simple model of a family.

## Win Condition

A marker summarizes the score in a range from absolute poverty to absolute wealth.

Doubling of purchasing-power-parity income from a benchmark near the poorest one percent. As a benchmark of the poorest annual income, $100 USD PPP in 2013 is benchmarked at 0. So an income of $200 doubles 1 time, $400 doubles 2 times. The average of those two humans would be 1.5. This attempts to incentivize investments in both the poor and rich, with a heavy emphasis on investing in the poor.

In this simulation, all monetary values are normalized to some estimates of USD PPP sometime between 2013 and 2020. 

GiveWell scores by averaging doublings of consumption from the poorest. The book Factfulness does something analogous, bracketing a [level as a quadrupling of income](https://www.gapminder.org/topics/four-income-levels).

Stats on income appear to be quicker to find, so income may be used in place of consumption. Economists subtract taxes and savings from income to arrive at [consumption.](https://en.wikipedia.org/wiki/Consumption_(economics)).

To account for mourning the recently deceased, deceased members continue to be averaged at zero for their life expectancy.

## End Condition

100 years.

## Taking a Turn

1 year passes per second.

## Human

### Human Properties

#### Income

- Income
- Doublings Of Poorest Income
- Normalized Income

#### Reproduction

- Age

Age is represented as a vertical position from bottom 0 to top 100.

- Gender: Female or male.
- [Birth Rate](https://en.wikipedia.org/wiki/Birth_rate)
- Fertile Age Range
    - Min
    - Max

To simply simulate probabilities, the expected value will be used for rates. For example, with a birth rate of 4%, the would be a birth per 25 life-years. Since it is a binary decision, the two outcomes are averaged. When the average would equal 4% and there is an eligible female, a child birth is attempted. Example, with 1 male and 1 female both at 12 years of age, that is 24 life-years. The actual birth rate is 0%. The expected birth rate is 4%. Yet the preceding 24 life-years had 0% birth rate, which draws down the average. To maintain the average, about 12 years would be above the expected birth rate and about 12 years below the expected birth rate. So the first child would be attempted at about 13 life-years, and the next around 38 life-years. Life-years is the sum of the ages of all living.

# Future Features

This simulation focuses on small scope, simplicity, and publicly available evidence.

The following features have not been prioritized or implemented.

## Future Human Features

### Future Human Properties

- Literacy
- Children
- Living Expenses
- Parents
- Life Expectancy
- Alive
- Survival Rate
- Capital: Multiplier of income.
- Wealth
- Independence (dependents need care)

### Future Human Actions

- Die.
- Survive.
- Care for a child or a parent.
- Work.
- Have a child.
- Learn to read.
- Play.
