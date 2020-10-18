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

## End Condition

100 years.

## Taking a Turn

1 year passes per second.

# Future Features

This simulation focuses on small scope, simplicity, and publicly available evidence.

The following features have not been prioritized or implemented.

## Win Condition

Doubling of purchasing-power-parity income from a benchmark near the poorest one percent. As a benchmark of the poorest, $100 USD PPP in 2013 is benchmarked at 0. So an income of $200 doubles 1 time, $400 doubles 2 times. The average of those two humans would be 1.5. This attempts to incentivize investments in both the poor and rich, with a heavy emphasis on investing in the poor.

In this simulation, all monetary values are normalized to some estimates of USD PPP sometime between 2013 and 2020. 

GiveWell scores by averaging doublings of consumption from the poorest. The book Factfulness does something analogous, bracketing a [level as a quadrupling of income](https://www.gapminder.org/topics/four-income-levels).

Stats on income appear to be quicker to find, so income may be used in place of consumption. Economists subtract taxes and savings from income to arrive at [consumption.](https://en.wikipedia.org/wiki/Consumption_(economics)).

To account for mourning the recently deceased, deceased members continue to be averaged at zero for their life expectancy.

## Human

### Human Properties

- Income
- Doublings Of Poorest Income
- Alive
- Age
- Life Expectancy
- Gender: Female or male.
- Capital: Multiplier of income.
- Wealth
- Independence (dependents need care)
- Living Expenses
- Birth Rate
- Literacy
- Children
- Parents

### Human Actions

- Die.
- Survive.
- Care for a child or a parent.
- Work.
- Have a child.
- Learn to read.
- Play.
