﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumSharper.Conditions;

public sealed class ContextualWait<TSearchContext>
    where TSearchContext : ISearchContext
{
    private readonly TSearchContext _searchContext;

    private readonly IWait<TSearchContext> _wait;

    public ContextualWait(TSearchContext searchContext, TimeSpan timeout)
    {
        _searchContext = searchContext;

        _wait = new DefaultWait<TSearchContext>(_searchContext)
        {
            Timeout = timeout
        };
    }

    public IWait<TSearchContext> Wait { get { return _wait; } }

    public WebElementConditionBuilder<TSearchContext, IWebElement> Until(Func<TSearchContext, IWebElement> action)
    {
        return new WebElementConditionBuilder<TSearchContext, IWebElement>(this, action);
    }

    public StringConditionBuilder<TSearchContext, string> Until(Func<TSearchContext, string> action)
    {
        return new StringConditionBuilder<TSearchContext, string>(this, action);
    }

    public WebElementsConditionBuilder<TSearchContext, IReadOnlyCollection<IWebElement>> Until(Func<TSearchContext, IReadOnlyCollection<IWebElement>> action)
    {
        return new WebElementsConditionBuilder<TSearchContext, IReadOnlyCollection<IWebElement>>(this, action);
    }
}
