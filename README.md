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
- Income correlates to age.
    - Delta income.
- Birth rate correlates to income.
- Life expectancy correlates to income by a [Preston curve](https://en.wikipedia.org/wiki/Preston_curve).
    - Income may only be a indirect correlation, as seen by [correlations to sanitation, infant survival](https://blog.euromonitor.com/economic-growth-and-life-expectancy-do-wealthier-countries-live-longer/).

#### Income Sharing

Income redistributed within family, average for all after other adjustments. Previously, income didn't account for spending from parent to child, so the average doublings of income would be biased a bit lower for children, except that the doubling is clamped at least $100 income.

#### GiveDirectly

Donate to income by [GiveWell's model of GiveDirectly](https://docs.google.com/spreadsheets/d/1BmFwVYeGMkpys6hG0tnfHyq__ZFZf-bmXYLSHODGpLY/edit#gid=1680005064&range=B20:B24).
- 83% of cash reaches the family. Donating $1205 sends $1000 to the family.
- About $210 per person of household size 4.7 for each $1000 reaching a household.
- Baseline income $290 per person.
- First Year +$130. Ln Consumption +0.37 (Log2: +0.53)
- Next 9 years +$8.30. Ln Consumption +0.03 (Log2: +0.04)

Schedule income adjustments in future years. These are applied after other inputs.
For simplicity:
- Applies at start of each calendar year
- list of adjustments for future years

#### Reproduction

- Age
- Life Expectancy

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

## Recurring Donations

Button:
- Do Not Donate

Repeat the last button pressed each new year.

Donation budget.

Donation depletes budget by amount.

Cost on each button.

Income to donate each year.

## Future Human Features

### Malaria Simulator

70% of all humans are malaria free.

Income correlates to malaria protection.
- [1% income increase](https://docs.google.com/spreadsheets/d/1BmFwVYeGMkpys6hG0tnfHyq__ZFZf-bmXYLSHODGpLY/edit#gid=1364064522&range=A111:B114) for malaria protection.

Life expectancy correlates to malaria protection.
- 3% higher life expectancy, as a naive estimate derived from:
- [6% deaths averted](https://docs.google.com/spreadsheets/d/1BmFwVYeGMkpys6hG0tnfHyq__ZFZf-bmXYLSHODGpLY/edit#gid=1364064522&range=A216:B216).

Donate to Against Malaria Foundation.
- [$1.20 covers 1 person with a mosquito net](https://docs.google.com/spreadsheets/d/1BmFwVYeGMkpys6hG0tnfHyq__ZFZf-bmXYLSHODGpLY/edit#gid=1364064522&range=A11:B18).

### Worm Simulator

80% of all humans are worm free.

Income correlates to worms.
- [0.5% income increase](https://docs.google.com/spreadsheets/d/1BmFwVYeGMkpys6hG0tnfHyq__ZFZf-bmXYLSHODGpLY/edit#gid=472531943&range=A7:B15) for deworming.

Donate to Deworm the World.
- [$0.20 deworms one child one year](https://docs.google.com/spreadsheets/d/1BmFwVYeGMkpys6hG0tnfHyq__ZFZf-bmXYLSHODGpLY/edit#gid=472531943&range=A37:B47)

### Literacy Simulator

80% of all humans are literate.

Income correlates to literacy.
- [1% income](http://bibliotecadigital.fgv.br/ocs/index.php/sbe/EBE08/paper/viewFile/407/55)

Birth rate [correlates to female literacy](https://amity.edu/UserFiles/admaa/54a1bPaper%206.pdf).
- [2% lower birth rate for a literate female, 0% for a literate male](https://www.ncbi.nlm.nih.gov/pmc/articles/PMC4649870/)

Life expectancy correlates to literacy.
- Maybe this is accounted for in the income effect on life expectancy.

Donate to Pratham.
- [Cost $100 per literate child](https://prathamusa.org/program/literacy-and-learning/)
